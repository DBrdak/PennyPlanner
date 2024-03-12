namespace PennyPlanner.Application.Abstractions.Authentication;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string passwordInput, string passwordHash);
}