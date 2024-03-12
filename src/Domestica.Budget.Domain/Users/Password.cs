using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Exceptions.DB;

namespace Domestica.Budget.Domain.Users
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
