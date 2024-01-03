using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetify.Domain.Accounts
{
    public sealed record AccountError(string Message)
    {
        public static AccountError AccountNotFound(string id) => new ($"Account with id: '{id}' not exists.");
        public static AccountError AccountCannotBeUpdated(string id) => new ($"Account with id: '{id}' cannot be updated.");
        public static AccountError AccountCannotBeAdded(string id) => new ($"Account with id: '{id}' cannot be added.");
        public static AccountError AccountCannotBeDeleted(string id) => new($"Account with id: '{id}' cannot be deleted.");
        public static AccountError AccountWithSameNameAlreadyExists(AccountName name) => new($"Account with name: '{name.Value}' already exists.");
    }
}
