using Domestica.Budget.Domain.Users;
using Responses.DB;

namespace Domestica.Budget.Application.Abstractions.Authentication;

public interface IJwtService
{
    Task<Result<string>> GetAccessTokenAsync(
        User userToAuthenticate,
        string password,
        CancellationToken cancellationToken = default);
}