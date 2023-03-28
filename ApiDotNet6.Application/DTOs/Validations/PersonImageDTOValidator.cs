using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Application.DTOs.Validations
{
    public class PersonImageDTOValidator: AbstractValidator<PersonImageDTO>
    {

        public PersonImageDTOValidator() {

            RuleFor(x => x.PersonId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Id da pessoa deve ser informado!");

            RuleFor(x => x.Image)
            .NotNull()
            .NotEmpty()
            .WithMessage("Imagem deve ser informado!");

        }

    }
}
