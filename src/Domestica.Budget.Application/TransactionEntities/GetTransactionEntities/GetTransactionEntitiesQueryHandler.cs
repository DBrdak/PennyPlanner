using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionEntities;
using Responses.DB;

namespace Domestica.Budget.Application.TransactionEntities.GetTransactionEntities
{
    internal sealed class GetTransactionEntitiesQueryHandler : IQueryHandler<GetTransactionEntitiesQuery, List<TransactionEntity>>
    {
        private readonly ITransactionEntityRepository _transactionEntityRepository;

        public GetTransactionEntitiesQueryHandler(ITransactionEntityRepository transactionEntityRepository)
        {
            _transactionEntityRepository = transactionEntityRepository;
        }

        public async Task<Result<List<TransactionEntity>>> Handle(GetTransactionEntitiesQuery request, CancellationToken cancellationToken)
        {
            //TODO Retrive user id
            return await _transactionEntityRepository.BrowseUserTransactionEntitiesAsync();
        }
    }
}
