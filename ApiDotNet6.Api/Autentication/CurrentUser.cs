using ApiDotNet6.Domain.Authentication;

namespace ApiDotNet6.Api.Autentication
{
    public class CurrentUser : ICurrentUser
    {
        public int Id { get;  set; }
        public string Email { get; set; }
        public string Permissions { get; set; }

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext;
            var claims = httpContext.User.Claims;

            if(claims.Any(x => x.Type == "Id"))
            {
                var id = Convert.ToInt32(claims.First(x => x.Type == "Id").Value);
                Id = id;

            }
            if (claims.Any(x => x.Type == "Email"))
            {
                var email = claims.First(x => x.Type == "Email").Value;
                Email = email;

            }

            if (claims.Any(x => x.Type == "Permission"))
            {
                var permissions = claims.First(x => x.Type == "Permission").Value;
                Permissions = permissions;

            }

        }
    }
}
