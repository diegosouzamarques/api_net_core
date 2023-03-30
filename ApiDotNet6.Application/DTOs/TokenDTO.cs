
namespace ApiDotNet6.Application.DTOs
{
    public class TokenDTO
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
    }
}
