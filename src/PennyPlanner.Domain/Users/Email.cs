using System.Text.RegularExpressions;
using Exceptions.DB;

namespace PennyPlanner.Domain.Users;

public record Email
{
    public string Value { get; init; }
    private const string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

    public Email(string Value)
    {
        if (!IsValid(Value))
        {
            throw new DomainException<Email>($"Address email: {Value} is invalid");
        }

        this.Value = Value;
    }

    private static bool IsValid(string value) => Regex.IsMatch(value, emailPattern);
}