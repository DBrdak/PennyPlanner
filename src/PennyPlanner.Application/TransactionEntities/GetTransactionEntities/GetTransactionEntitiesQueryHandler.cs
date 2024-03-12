using CommonAbstractions.DB.Messaging;
using PennyPlanner.Domain.TransactionEntities;
using Responses.DB;

namespace PennyPlanner.Application.TransactionEntities.GetTransactionEntities
{
    internal sealed class GetTransactionEntitiesQueryHandler : IQueryHandler<GetTransactionEntitiesQuery, List<TransactionEntityModel>>
    {
        private readonly ITransactionEntityRepository _transactionEntityRepository;

        public GetTransactionEntitiesQueryHandler(ITransactionEntityRepository transactionEntityRepository)
        {
            _transactionEntityRepository = transactionEntityRepository;
        }

        public async Task<Result<List<TransactionEntityModel>>> Handle(GetTransactionEntitiesQuery request, CancellationToken cancellationToken)
        {
            //TODO Retrive user id
            var transactionEntities = await _transactionEntityRepository.BrowseUserTransactionEntitiesAsync();

            return transactionEntities.Select(TransactionEntityModel.FromDomainObject).ToList();
        }
    }
}
