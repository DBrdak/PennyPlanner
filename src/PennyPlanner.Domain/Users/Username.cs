using System.Text.RegularExpressions;
using Exceptions.DB;

namespace PennyPlanner.Domain.Users;

public record Username
{
    public string Value { get; init; }
    private const string usernamePattern = @"^(?=.{1,30}$)[^\s][\p{L}\d\s]*[^\s]$";

    public Username(string value)
    {
        if (!IsValid(value))
        {
            throw new DomainException<Username>($"Username: {Value} is invalid");
        }

        Value = value;
    }

    private static bool IsValid(string value) => Regex.IsMatch(value, usernamePattern);
}