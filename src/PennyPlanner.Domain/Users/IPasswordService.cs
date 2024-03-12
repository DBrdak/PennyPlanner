namespace PennyPlanner.Domain.Users;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string passwordInput, string passwordHash);
}