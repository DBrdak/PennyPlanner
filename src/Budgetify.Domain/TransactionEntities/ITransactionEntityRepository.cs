using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgetify.Domain.TransactionEntities
{
    public interface ITransactionEntityRepository
    {
        Task<TransactionEntity> GetByIdAsync(TransactionEntityId id, CancellationToken cancellationToken = default);

        Task<List<TransactionEntity>> BrowseUserTransactionEntitiesAsync(/*UserId userId*/);
        Task AddAsync(TransactionEntity entity);
        //Task<IEnumerable<TransactionEntity>> BrowseForUserAsync(User user);
    }
}
