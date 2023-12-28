using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetify.Domain.Shared
{
    public sealed record Frequency
    {
        public static readonly Frequency Daily = new("Daily", 1);
        public static readonly Frequency Weekly = new("Weekly", 7);
        public static readonly Frequency Monthly = new("Monthly", 28);
        public static readonly Frequency Quarterly = new("Quarterly", 90);
        public static readonly Frequency Yearly = new("Yearly", 365);

        private Frequency(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; init; }
        public int Value { get; init; }

        public static Frequency FromValue(int value)
        {
            return All.FirstOrDefault(f => f.Value == value) ??
                   throw new ApplicationException("The frequency is invalid");
        }

        public static readonly IReadOnlyCollection<Frequency> All = new[]
        {
            Daily,
            Weekly,
            Monthly,
            Quarterly,
            Yearly
        };
    }

}
