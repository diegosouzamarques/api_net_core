using ApiDotNet6.Domain.Entities;

namespace ApiDotNet6.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User> CreateAsync(User user);
        Task<User> TokenRegisterAsync(User user);
        Task<User> GetUserByRefreshTokenAsync(string refreshTokenDTO);
        Task<User> EditAsync(User user);

    }
}
