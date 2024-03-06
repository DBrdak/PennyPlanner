using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domestica.Budget.Application.Settings.ValidationSettings;
using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Application.Users.RegisterUser
{
    public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .Matches(UserValidationSettings.EmailPattern)
                .WithMessage("Invalid email");

            RuleFor(x => x.Password)
                .Matches(UserValidationSettings.PasswordPattern)
                .WithMessage("Password does not fulfill the requirements");
        }
    }
}
