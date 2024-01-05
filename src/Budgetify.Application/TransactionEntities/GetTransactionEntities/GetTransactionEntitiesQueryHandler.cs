using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.TransactionEntities;
using CommonAbstractions.DB.Messaging;
using Responses.DB;

namespace Budgetify.Application.TransactionEntities.GetTransactionEntities
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
