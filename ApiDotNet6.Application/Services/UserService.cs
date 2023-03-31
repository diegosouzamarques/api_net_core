using ApiDotNet6.Application.DTOs;
using ApiDotNet6.Application.DTOs.Validations;
using ApiDotNet6.Application.Services.Interface;
using ApiDotNet6.Domain.Entities;
using ApiDotNet6.Domain.Repositories;
using AutoMapper;
using System.Linq;

namespace ApiDotNet6.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IGenerateToken _tokenGenerator;
        private readonly IMapper _mapper;
        private readonly IPasswordHash _passwordHashService;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IGenerateToken tokenGenerator, IMapper mapper, IPasswordHash passwordHash, IPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;  
            _mapper = mapper;
            _passwordHashService = passwordHash;
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<ICollection<PermissionDTO>>> PermissionAsync()
        {

            var permissions = await _permissionRepository.GetPermissionAsync();
            return ResultService.Ok(_mapper.Map<ICollection<PermissionDTO>>(permissions));
            
        }

        public async Task<ResultService<TokenDTO>> RefreshToken(RefreshTokenDTO refreshTokenDTO)
        {
            if (refreshTokenDTO == null)
                return ResultService.Fail<TokenDTO>("Objeto deve ser informado!");

            var validator = new RefreshTokenDTOValidator().Validate(refreshTokenDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<TokenDTO>("Problemas com a validadção", validator);

            var exisitingUser = await _userRepository.GetUserByRefreshTokenAsync(refreshTokenDTO.RefreshToken);

            if (exisitingUser == null)
                return ResultService.Fail<TokenDTO>("Refresh token inválido");

            if (exisitingUser.TokenExpires < DateTime.Now)
                return ResultService.Fail<TokenDTO>("Token expirado");

            var userToken = _tokenGenerator.GenerateAccessToken(exisitingUser);

            exisitingUser.Edit(exisitingUser.Id, userToken.RefreshToken, userToken.Created, userToken.Expires);

            await _userRepository.TokenRegisterAsync(exisitingUser);

            return ResultService.Ok(userToken);

        }

        public async Task<ResultService<UserDTO>> Register(UserDTO userDTO)
        {
            if (userDTO == null)
                return ResultService.Fail<UserDTO>("Objeto deve ser informado!");

            var validator = new UserDTOValidator().Validate(userDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<UserDTO>("Problemas com a validadção", validator);

            var exisitingUser = await _userRepository.GetUserByUsernameAsync(userDTO.Username);
            if (exisitingUser != null)
                return ResultService.Fail<UserDTO>("Nome de usuário já cadastrado.");

            var existPermission = await _permissionRepository.GetExistAsync(userDTO.Permissions);
            if (existPermission.Count <= 0)
                return ResultService.Fail<UserDTO>("Nenhuma permissão informada existente.");

            _passwordHashService.HashPassword(userDTO.Password!, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User(userDTO.Email, userDTO.Password, userDTO.Username, passwordHash, passwordSalt);

            try {
                await _unitOfWork.BeginTransaction();

                var data = await _userRepository.CreateAsync(user);

                existPermission.ToList().ForEach((permission) =>
                {
                    var permissionUser = new UserPermission(data.Id, permission.Id);
                    data.UserPermissions.Add(permissionUser);
                });

                await _userRepository.EditAsync(data);
              
                await _unitOfWork.Commit();
               
                return ResultService.Ok<UserDTO>(userDTO);
            } catch (Exception ex) {
                await _unitOfWork.Rollback();
                return ResultService.Fail<UserDTO>(ex.Message);
            }     
        }

        public async Task<ResultService<TokenDTO>> Signin(UserSigninDTO userSigninDTO)
        {
            if (userSigninDTO == null)
                return ResultService.Fail<TokenDTO>("Objeto deve ser informado!");

            var validator = new UserSigninDTOValidator().Validate(userSigninDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<TokenDTO>("Problemas com a validadção", validator);

            var exisitingUser = await _userRepository.GetUserByUsernameAsync(userSigninDTO.Username);
            if (exisitingUser == null)
                return ResultService.Fail<TokenDTO>("Usuário não encontrado.");

            if (!_passwordHashService.VerifyPasssword(userSigninDTO.Password, exisitingUser.PasswordHash, exisitingUser.PasswordSalt))
                return ResultService.Fail<TokenDTO>("Senha incorreta!");

            var userToken = _tokenGenerator.GenerateAccessToken(exisitingUser);

            exisitingUser.Edit(exisitingUser.Id, userToken.RefreshToken, userToken.Created, userToken.Expires);

            await _userRepository.TokenRegisterAsync(exisitingUser);


            return ResultService.Ok(userToken);
        }
   
    
    }
}
