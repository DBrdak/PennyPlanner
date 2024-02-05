using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.DataTransferObjects;

namespace Domestica.Budget.Application.Transactions.GetTransactions
{
    public sealed record GetTransactionsQuery() : IQuery<List<TransactionDto>>
    {
    }
}
