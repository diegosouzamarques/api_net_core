using FluentValidation;

namespace ApiDotNet6.Application.DTOs.Validations
{
    public class PurchaseDTOValidator : AbstractValidator<PurchaseDTO> 
    {
        public PurchaseDTOValidator() {
            RuleFor(x => x.CodErp)
                .NotEmpty()
                .NotNull()
                .WithMessage("CodErp deve ser informado!");

            RuleFor(x => x.Document)
                .NotEmpty()
                .NotNull()
                .WithMessage("Documento deve ser informado!")
                .MaximumLength(20)
                .WithMessage("Documento máximo 20 caracteres");
        }

    }
}
