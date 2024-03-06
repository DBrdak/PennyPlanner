using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.Transactions;
using Responses.DB;

namespace Domestica.Budget.Application.Transactions.GetTransactions
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
