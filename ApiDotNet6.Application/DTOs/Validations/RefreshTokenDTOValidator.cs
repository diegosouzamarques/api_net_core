using FluentValidation;

namespace ApiDotNet6.Application.DTOs.Validations
{
    public class RefreshTokenDTOValidator: AbstractValidator<RefreshTokenDTO>
    {
        public RefreshTokenDTOValidator()
        {
            RuleFor(x => x.RefreshToken)
            .NotNull()
            .NotEmpty()
            .WithMessage("Token refresh deve ser informado!");
        }

    }
}
