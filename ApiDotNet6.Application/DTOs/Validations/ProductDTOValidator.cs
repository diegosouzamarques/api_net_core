using FluentValidation;

namespace ApiDotNet6.Application.DTOs.Validations
{
    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidator()
        {
            RuleFor(x => x.CodErp)
                .NotNull()
                .NotEmpty()
                .WithMessage("CodErp deve ser informado")
                .MaximumLength(10)
                .WithMessage("CodErp máximo 10 caracteres");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Nome deve ser informado")
                .MaximumLength(100)
                .WithMessage("Nome máximo 100 caracteres");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price deve ser maior que zero");
        }
    }
}
