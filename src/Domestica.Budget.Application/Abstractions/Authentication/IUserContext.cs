namespace Domestica.Budget.Application.Abstractions.Authentication;

public interface IUserContext
{
    string IdentityId { get; }
    string UserCurrencyCode { get; }
    string? TryGetIdentityId();
}