﻿using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Domain.TransactionCategories;
using Responses.DB;

namespace Domestica.Budget.Application.TransactionCategories.GetTransactionCategories
{
    internal sealed class GetTransactionCategoriesQueryHandler : IQueryHandler<GetTransactionCategoriesQuery, IEnumerable<TransactionCategoryModel>>
    {
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;

        public GetTransactionCategoriesQueryHandler(ITransactionCategoryRepository transactionCategoryRepository)
        {
            _transactionCategoryRepository = transactionCategoryRepository;
        }

        public async Task<Result<IEnumerable<TransactionCategoryModel>>> Handle(GetTransactionCategoriesQuery request, CancellationToken cancellationToken)
        {
            var transactionCategories = await _transactionCategoryRepository.BrowseAllIncludeAsync(
                c => c.Subcategories,
                cancellationToken);

            var dtos = transactionCategories.ToList().Select(TransactionCategoryModel.FromDomainObject);

            return Result.Create(dtos)!;
        }
    }
}
