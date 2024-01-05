﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Budgetify.Application.Accounts.AddAccount
{
    public sealed record AccountType
    {
        public string Value { get; init;  }

        [JsonConstructor]
        private AccountType(string value) =>
            Value = value;

        public static readonly AccountType Savings = new("Savings");
        public static readonly AccountType Transactional = new("Transactional");
    }
}
