using Budgetify.Domain.Accounts;
using Budgetify.Domain.TransactionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetify.Domain.Transactions
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> BrowseAccountTransactionsAsync(AccountId accountId);
        Task<IEnumerable<Transaction>> BrowseSenderTransactionsAsync(TransactionEntityId senderId);
        Task<IEnumerable<Transaction>> BrowseRecipientTransactionsAsync(TransactionEntityId recipientId);
    }
}
