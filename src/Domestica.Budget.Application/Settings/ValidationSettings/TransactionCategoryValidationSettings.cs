using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domestica.Budget.Application.Settings.ValidationSettings
{
    internal static class TransactionCategoryValidationSettings
    {
        internal static byte TransactionCategoryNameMaxLength = 30;
        internal static string TransactionCategoryNamePattern = @"^[^\s][\p{L}\d\s]*[^\s]$";
    }
}
