using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.DataTransferObjects;
using Domestica.Budget.Domain.TransactionEntities;
using Responses.DB;

namespace Domestica.Budget.Application.TransactionEntities.GetTransactionEntities
{
    internal sealed class GetTransactionEntitiesQueryHandler : IQueryHandler<GetTransactionEntitiesQuery, List<TransactionEntityDto>>
    {
        private readonly ITransactionEntityRepository _transactionEntityRepository;

        public GetTransactionEntitiesQueryHandler(ITransactionEntityRepository transactionEntityRepository)
        {
            _transactionEntityRepository = transactionEntityRepository;
        }

        public async Task<Result<List<TransactionEntityDto>>> Handle(GetTransactionEntitiesQuery request, CancellationToken cancellationToken)
        {
            //TODO Retrive user id
            var transactionEntities = await _transactionEntityRepository.BrowseUserTransactionEntitiesAsync();

            return transactionEntities.Select(TransactionEntityDto.FromDomainObject).ToList();
        }
    }
}
