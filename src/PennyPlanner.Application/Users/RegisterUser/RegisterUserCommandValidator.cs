using FluentValidation;
using Money.DB;
using PennyPlanner.Application.Settings.ValidationSettings;

namespace PennyPlanner.Application.Users.RegisterUser
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

            RuleFor(x => x.Username)
                .Matches(UserValidationSettings.UsernamePattern)
                .WithMessage("Invalid username");

            RuleFor(x => x.Currency)
                .Must(
                    c => Currency.All.Any(
                        currency => string.Equals(
                            currency.Code,
                            c,
                            StringComparison.CurrentCultureIgnoreCase)))
                .WithMessage("Currency not supported");
        }
    }
}
