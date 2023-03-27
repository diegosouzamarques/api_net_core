using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Application.DTOs.Validations
{
    public class UserDTOValidator: AbstractValidator<UserDTO>
    {
        public UserDTOValidator() {

            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Email deve ser informado!");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Password deve ser informado!");
        }

    }
}
