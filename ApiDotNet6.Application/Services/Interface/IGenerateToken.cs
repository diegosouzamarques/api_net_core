using ApiDotNet6.Application.DTOs;
using ApiDotNet6.Domain.Entities;

namespace ApiDotNet6.Application.Services.Interface
{
    public interface IGenerateToken
    {
        public TokenDTO GenerateAccessToken(User user);
    }
}
