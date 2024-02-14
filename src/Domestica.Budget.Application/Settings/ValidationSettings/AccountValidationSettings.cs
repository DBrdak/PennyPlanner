using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domestica.Budget.Application.Settings.ValidationSettings
{
    internal static class AccountValidationSettings
    {
        internal static byte AccountNameMaxLength = 30;
        internal static string AccountNamePattern = @"^[^\s][\p{L}\d\s]*[^\s]$";
    }
}
