using System.Security.Claims;
using Domestica.Budget.Domain.Users;

namespace Domestica.Budget.Infrastructure.Authentication.Models;

public sealed record UserRepresentationModel
{
    private readonly string _cratedTimeStampClaimName = nameof(CreatedTimestamp);
    public long CreatedTimestamp { get; init; }

    private readonly string _emailClaimName = nameof(Email);
    public string Email { get; init; }

    private readonly string _emailVerifiedClaimName = nameof(EmailVerified);
    public bool EmailVerified { get; init; }

    private readonly string _idClaimName = nameof(Id);
    public string Id { get; init; }

    private readonly string _usernameClaimName = nameof(Username);
    public string Username { get; init; }

    private readonly string _currencyClaimName = nameof(Currency);
    public string Currency { get; init; }

    private UserRepresentationModel(long createdTimestamp, string email, bool emailVerified, string id, string username, string currency)
    {
        CreatedTimestamp = createdTimestamp;
        Email = email;
        EmailVerified = emailVerified;
        Id = id;
        Username = username;
        Currency = currency;
    }

    internal static UserRepresentationModel FromUser(User user) =>
        new(
            DateTimeOffset.UtcNow.ToUnixTimeSeconds(),
            user.Email.Value,
            true,
            user.Id.Value.ToString(),
            user.Email.Value,
            user.Currency.Code);

    internal Claim[] ToClaims()
    {
        return new[]
        {
            new Claim(_cratedTimeStampClaimName, CreatedTimestamp.ToString()),
            new Claim(_emailClaimName, Email),
            new Claim(_emailVerifiedClaimName, EmailVerified.ToString()),
            new Claim(_idClaimName, Id),
            new Claim(_usernameClaimName, Username),
            new Claim(_currencyClaimName, Currency),
        };
    }
}