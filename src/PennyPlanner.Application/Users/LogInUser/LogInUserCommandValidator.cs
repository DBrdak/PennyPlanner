using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using PennyPlanner.Application.Settings.ValidationSettings;

namespace PennyPlanner.Application.Users.LogInUser
{
    public sealed class LogInUserCommandValidator : AbstractValidator<LogInUserCommand>
    {
        public LogInUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .Matches(UserValidationSettings.EmailPattern)
                .WithMessage("Invalid email");
        }
    }
}
