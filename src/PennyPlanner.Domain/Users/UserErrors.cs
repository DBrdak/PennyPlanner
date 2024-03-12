using Responses.DB;

namespace PennyPlanner.Domain.Users;

public static class UserErrors
{
    public static Error NotFound = Error.NotFound(
        "The user with the specified identifier was not found");

    public static Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "Invalid credentials");
}