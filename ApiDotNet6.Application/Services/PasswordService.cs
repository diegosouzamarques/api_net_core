using ApiDotNet6.Application.Services.Interface;
using System.Security.Cryptography;
using System.Text;

namespace ApiDotNet6.Application.Services
{
    public class PasswordService : IPasswordHash
    {
        public void HashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasssword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return ComputeHash.SequenceEqual(passwordHash);
            }
        }
    }
}
