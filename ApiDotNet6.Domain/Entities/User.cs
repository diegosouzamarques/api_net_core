using ApiDotNet6.Domain.Validations;

namespace ApiDotNet6.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public byte[]? PasswordHash { get; private set; }
        public byte[]? PasswordSalt { get; private set; }
        public string? Role { get; private set; }
        public string? RefreshToken { get; private set; }
        public DateTime? DateCreated { get; private set; }
        public DateTime? TokenExpires { get; private set; }

        public ICollection<UserPermission> UserPermissions { get; set; }


        public void Edit(int id, string refreshToken, DateTime? dateCreated, DateTime? tokenExpires)
        {            
            DomainValidationException.When(id <= 0, "Id usuario deve ser informado!");

            DomainValidationException.When(string.IsNullOrEmpty(refreshToken), "Refresh token deve ser informado");
            DomainValidationException.When(dateCreated == null, "Data criação do token deve ser informado");
            DomainValidationException.When(tokenExpires < DateTime.Now, "Data expiração deve ser informado");
            Id = id;
            RefreshToken = refreshToken;
            DateCreated = dateCreated;
            TokenExpires = tokenExpires;
        }


        public User(int id,string email, string password, string username)
        {
            DomainValidationException.When(id <= 0, "Id deve ser informado");
            Validation(email, password, username);
            Id = id;
            UserPermissions = new List<UserPermission>();
        }

        public User(string email, string password, string username, byte[]? passwordhash, byte[]? passwordsalt, string role)
        {
            Validation(email, password, username);
            DomainValidationException.When(string.IsNullOrEmpty(role), "Regra de usuário deve ser informado");
            DomainValidationException.When(passwordhash?.Length <= 0, "Password hash deve ser informado");
            DomainValidationException.When(passwordsalt?.Length <= 0, "Password salt  deve ser informado");
            PasswordHash = passwordhash;
            PasswordSalt = passwordsalt;
            Role = role;
            UserPermissions = new List<UserPermission>();
        }

        private void Validation(string email, string password, string username)
        {
            DomainValidationException.When(string.IsNullOrEmpty(username), "Nome de usuário deve ser informado");
            DomainValidationException.When(string.IsNullOrEmpty(email), "Email deve ser informado");
            DomainValidationException.When(string.IsNullOrEmpty(password), "Senha deve ser informado");


            Email = email;
            Password = password;
            Username = username;

    }
    }
}
