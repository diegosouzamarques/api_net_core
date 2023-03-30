using FluentValidation;

namespace ApiDotNet6.Application.DTOs.Validations
{
    public class UserDTOValidator: AbstractValidator<UserDTO>
    {
        public UserDTOValidator() {

            RuleFor(x => x.Username)
                .NotNull()
                .NotEmpty()
                .WithMessage("Nome de usuário deve ser informado!");

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Email deve ser informado!");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Password deve ser informado!");

            RuleFor(x => x.Role)
                .NotNull()
                .NotEmpty()
                .WithMessage("Password deve ser informado!");
        }

    }
}
