namespace PennyPlanner.Application.Settings.ValidationSettings
{
    internal static class AccountValidationSettings
    {
        internal static byte AccountNameMaxLength = 30;
        internal static string AccountNamePattern = @"^[^\s][\p{L}\d\s]*[^\s]$";
        internal static int AccountsLimit = 8;
    }
}
