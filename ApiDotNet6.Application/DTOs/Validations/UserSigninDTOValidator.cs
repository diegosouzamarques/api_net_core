using FluentValidation;

namespace ApiDotNet6.Application.DTOs.Validations
{
    public class UserSigninDTOValidator : AbstractValidator<UserSigninDTO>
    {
        public UserSigninDTOValidator()
        {

            RuleFor(x => x.Username)
                .NotNull()
                .NotEmpty()
                .WithMessage("Nome de usuário deve ser informado!");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Senha deve ser informado!");
        }
    }
 
}
