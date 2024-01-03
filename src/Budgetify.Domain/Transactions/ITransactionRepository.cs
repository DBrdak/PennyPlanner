using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetify.Domain.Transactions
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> BrowseAccountTransactionsAsync(string accountId);
        Task<IEnumerable<Transaction>> BrowseEntityTransactionsAsync(string transactionEntityId);
    }
}
