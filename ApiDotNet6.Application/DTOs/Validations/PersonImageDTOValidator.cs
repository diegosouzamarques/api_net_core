using FluentValidation;

namespace ApiDotNet6.Application.DTOs.Validations
{
    public class PersonImage64DTOValidator: AbstractValidator<PersonImage64DTO>
    {

        public PersonImage64DTOValidator() {

            RuleFor(x => x.PersonId)
            .LessThanOrEqualTo(0)
            .WithMessage("Id da pessoa deve ser informado!");

            RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("Descrição deve ser informado!")
            .MaximumLength(200)
            .WithMessage("Descrição máximo 200 caracteres");

            RuleFor(x => x.Image)
            .NotNull()
            .NotEmpty()
            .WithMessage("Imagem deve ser informado!");

        }

    }

    public class PersonImageFileDTOValidator : AbstractValidator<PersonImageFileDTO>
    {

        public PersonImageFileDTOValidator()
        {

            RuleFor(x => x.PersonId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Id da pessoa deve ser informado!");

            RuleFor(x => x.Description)
            .NotNull()
            .NotEmpty()
            .WithMessage("Imagem deve ser informado!");

            RuleFor(x => x.File)
            .NotNull()
            .NotEmpty()
            .WithMessage("Imagem deve ser informado!");

        }

    }
}
