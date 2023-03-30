using ApiDotNet6.Application.DTOs;

namespace ApiDotNet6.Application.Services.Interface
{
    public interface IUserService
    {
        Task<ResultService<UserDTO>> Register(UserDTO userDTO);
        Task<ResultService<TokenDTO>> Signin(UserSigninDTO userSigninDTO);
        Task<ResultService<TokenDTO>> RefreshToken(RefreshTokenDTO refreshTokenDTO);
    }
}
