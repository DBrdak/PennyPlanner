using System.Text.RegularExpressions;
using Exceptions.DB;

namespace PennyPlanner.Domain.Users
{
    public sealed record Password
    {
        public string Value { get; init; }
        private const string passwordPattern = @"^(?=.*[!@#$%^&*()-_=+{};:',.<>?])(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";

        private Password(string value)
        {
            Value = value;
        }

        public static Password Create(string value)
        {
            return Regex.IsMatch(value, passwordPattern) ?
                new Password(value) :
                throw new DomainException<Password>("Invalid password");
        }
    }
}
