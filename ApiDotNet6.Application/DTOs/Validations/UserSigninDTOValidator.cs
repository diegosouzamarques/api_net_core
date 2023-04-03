using FluentValidation;
using System.Text;

namespace ApiDotNet6.Application.DTOs.Validations
{
    public class UserSigninDTOValidator : AbstractValidator<UserSigninDTO>
    {
        public UserSigninDTOValidator()
        {

            RuleFor(x => x.basicAutenticate)
                .NotNull()
                .NotEmpty()
                .WithMessage("Usuário e senha em Basic Autenticate deve ser informado!")
                .Must(basicAutenticateOK).WithMessage("Usuário ou senha não informados!");

        }

        private bool basicAutenticateOK(string basicAutenticate)
        {
            try
            {
                var credentialBytes = Convert.FromBase64String(basicAutenticate);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);

                var username = credentials[0];
                var password = credentials[1];

                return !(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password));
            }catch
            {
                return false;
            }
            
        }
    }
 
}
