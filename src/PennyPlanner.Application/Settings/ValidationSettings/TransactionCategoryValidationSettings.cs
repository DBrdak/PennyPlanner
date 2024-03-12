namespace PennyPlanner.Application.Settings.ValidationSettings
{
    internal static class TransactionCategoryValidationSettings
    {
        internal static byte TransactionCategoryNameMaxLength = 30;
        internal static string TransactionCategoryNamePattern = @"^[^\s][\p{L}\d\s]*[^\s]$";
    }
}
