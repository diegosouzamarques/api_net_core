
namespace ApiDotNet6.Application.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<int> Permissions { get; set; }

        public UserDTO () {
            Permissions = new List<int> ();
        }

    }
}
