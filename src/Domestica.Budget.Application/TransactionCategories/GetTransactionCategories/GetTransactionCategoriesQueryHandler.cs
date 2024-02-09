using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.DataTransferObjects;
using Domestica.Budget.Domain.TransactionCategories;
using Responses.DB;

namespace Domestica.Budget.Application.TransactionCategories.GetTransactionCategories
{
    internal sealed class GetTransactionCategoriesQueryHandler : IQueryHandler<GetTransactionCategoriesQuery, IEnumerable<TransactionCategoryDto>>
    {
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;

        public GetTransactionCategoriesQueryHandler(ITransactionCategoryRepository transactionCategoryRepository)
        {
            _transactionCategoryRepository = transactionCategoryRepository;
        }

        public async Task<Result<IEnumerable<TransactionCategoryDto>>> Handle(GetTransactionCategoriesQuery request, CancellationToken cancellationToken)
        {
            var transactionCategories = (await _transactionCategoryRepository.BrowseAllAsync(cancellationToken)).ToList();

            var dtos = transactionCategories.Select(TransactionCategoryDto.FromDomainObject);

            return Result.Create(dtos)!;
        }
    }
}
