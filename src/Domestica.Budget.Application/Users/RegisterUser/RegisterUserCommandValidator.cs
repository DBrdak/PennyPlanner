using FluentValidation;
using Domestica.Budget.Application.Settings.ValidationSettings;
using Money.DB;

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
