
namespace ApiDotNet6.Application.Services.Interface
{
    public interface IPasswordHash
    {
        public void HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public bool VerifyPasssword(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
