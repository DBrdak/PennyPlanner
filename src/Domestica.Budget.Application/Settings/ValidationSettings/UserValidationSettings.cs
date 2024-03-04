using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domestica.Budget.Application.Settings.ValidationSettings
{
    internal class UserValidationSettings
    {
        internal static byte UsernameMaxLength = 30;
        internal static string UsernamePattern = @"^[^\s][\p{L}\d\s]*[^\s]$";
        internal static string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        internal static string PasswordPattern = @"^(?=.*[!@#$%^&*()-_=+{};:',.<>?])(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
    }
}
