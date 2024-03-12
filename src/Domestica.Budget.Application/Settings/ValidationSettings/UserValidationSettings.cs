namespace Domestica.Budget.Application.Settings.ValidationSettings
{
    internal class UserValidationSettings
    {
        internal static string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        internal static string PasswordPattern = @"^(?=.*[!@#$%^&*()-_=+{};:',.<>?])(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
        internal static string UsernamePattern = @"^(?=.{1,30}$)[^\s][\p{L}\d\s]*[^\s]$";
    }
}
