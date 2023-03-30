using ApiDotNet6.Application.DTOs;
using ApiDotNet6.Application.DTOs.Validations;
using ApiDotNet6.Application.Services.Interface;
using ApiDotNet6.Domain.Entities;
using ApiDotNet6.Domain.Repositories;
using AutoMapper;

namespace ApiDotNet6.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenerateToken _tokenGenerator;
        private readonly IMapper _mapper;
        private readonly IPasswordHash _passwordHashService;

        public UserService(IUserRepository userRepository, IGenerateToken tokenGenerator, IMapper mapper, IPasswordHash passwordHash)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;  
            _mapper = mapper;
            _passwordHashService = passwordHash;
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
         
            _passwordHashService.HashPassword(userDTO.Password!, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User(userDTO.Email, userDTO.Password, userDTO.Username, passwordHash, passwordSalt, userDTO.Role);

            var data = await _userRepository.CreateAsync(user);
            return ResultService.Ok<UserDTO>(_mapper.Map<UserDTO>(data));

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
