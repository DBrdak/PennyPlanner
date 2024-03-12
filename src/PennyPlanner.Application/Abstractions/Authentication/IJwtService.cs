using PennyPlanner.Domain.Users;
using Responses.DB;

namespace PennyPlanner.Application.Abstractions.Authentication;

public interface IJwtService
{
    Task<Result<string>> GetAccessTokenAsync(
        User userToAuthenticate,
        string password,
        CancellationToken cancellationToken = default);
}