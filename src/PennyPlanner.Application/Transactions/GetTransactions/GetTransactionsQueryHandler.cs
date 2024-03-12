using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.Transactions;
using Responses.DB;

namespace PennyPlanner.Application.Transactions.GetTransactions
{
    internal sealed class GetTransactionsQueryHandler : IQueryHandler<GetTransactionsQuery, List<TransactionModel>>
    {
        private readonly ITransactionRepository _transactionRepository;

        public GetTransactionsQueryHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Result<List<TransactionModel>>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _transactionRepository.BrowseUserTransactions(cancellationToken);

            return transactions
                .Select(TransactionModel.FromDomainObject)
                .ToList();
        }
    }
}
