using FluentValidation;

namespace ApiDotNet6.Application.DTOs.Validations
{
    public class PersonDTOValidator : AbstractValidator<PersonDTO>
    {
        public PersonDTOValidator()
        {
            RuleFor(x => x.Document)
                .NotEmpty()
                .NotNull()
                .WithMessage("Documento deve ser informado!")
                .MaximumLength(20)
                .WithMessage("Documento máximo 20 caracteres");

            RuleFor(x => x.Name)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Nome deve ser informado!")
                    .MaximumLength(100)
                    .WithMessage("Nome máximo 100 caracteres");
             
            RuleFor(x => x.Phone)
                 .NotNull()
                 .NotEmpty()
                 .WithMessage("Celular deve ser informado!")
                .MaximumLength(20)
                .WithMessage("Celular máximo 20 caracteres");
        }
    }
}
