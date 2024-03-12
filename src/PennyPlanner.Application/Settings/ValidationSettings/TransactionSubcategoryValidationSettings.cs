namespace PennyPlanner.Application.Settings.ValidationSettings
{
    internal static class TransactionSubcategoryValidationSettings
    {
        internal static byte TransactionSubcategoryNameMaxLength = 30;
        internal static string TransactionSubcategoryNamePattern = @"^[^\s][\p{L}\d\s]*[^\s]$";
    }
}
