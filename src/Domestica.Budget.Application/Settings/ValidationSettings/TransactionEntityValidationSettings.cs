using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domestica.Budget.Application.Settings.ValidationSettings
{
    internal static class TransactionEntityValidationSettings
    {
        internal static byte TransactionEntityNameMaxLength = 30;
        internal static string TransactionEntityNamePattern = @"^[^\s][\p{L}\d\s]*[^\s]$";
    }
}
