using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domestica.Budget.Application.Settings.ValidationSettings
{
    internal static class TransactionSubcategoryValidationSettings
    {
        internal static byte TransactionSubcategoryNameMaxLength = 30;
        internal static string TransactionSubcategoryNamePattern = @"^[^\s][\p{L}\d\s]*[^\s]$";
    }
}
