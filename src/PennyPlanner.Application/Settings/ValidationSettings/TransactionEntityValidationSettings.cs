namespace PennyPlanner.Application.Settings.ValidationSettings
{
    internal static class TransactionEntityValidationSettings
    {
        internal static byte TransactionEntityNameMaxLength = 30;
        internal static string TransactionEntityNamePattern = @"^[^\s][\p{L}\d\s]*[^\s]$";
    }
}
