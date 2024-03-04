using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domestica.Budget.Application.Settings.ValidationSettings;

namespace Domestica.Budget.Application.Users.RegisterUser
{
    public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(c => c.Username)
                .MaximumLength(UserValidationSettings.UsernameMaxLength)
                .WithMessage("Username is too long")
                .Matches(UserValidationSettings.UsernamePattern)
                .WithMessage("Invalid username");

            RuleFor(c => c.Email)
                .EmailAddress()
                .WithMessage("Invalid email");

            RuleFor(c => c.Password)
                .Matches(UserValidationSettings.PasswordPattern)
                .WithMessage("Password does not fulfill the requirements");
        }
    }
}
