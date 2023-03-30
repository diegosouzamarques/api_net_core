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
                .WithMessage("CodErp deve ser informado");

            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Name deve ser informado");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price deve ser maior que zero");
        }
    }
}
