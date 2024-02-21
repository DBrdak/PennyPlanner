using System.Diagnostics.CodeAnalysis;
using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.TransactionCategories.AddTransactionCategory;
using Domestica.Budget.Application.TransactionEntities.AddTransactionEntity;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.TransactionEntities.TransactionRecipients;
using Domestica.Budget.Domain.Transactions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Money.DB;
using Responses.DB;
using System.Reflection;
using Domestica.Budget.Domain.TransactionSubcategories;

namespace Domestica.Budget.Application.Transactions.AddOutcomeTransaction
{
    internal sealed class AddOutcomeTransactionCommandHandler : ICommandHandler<AddOutcomeTransactionCommand, Transaction>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionEntityRepository _transactionEntityRepository;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ITransactionCategoryRepository _categoryRepository;
        private readonly ITransactionSubcategoryRepository _subcategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddOutcomeTransactionCommandHandler(IAccountRepository accountRepository, ITransactionEntityRepository transactionEntityRepository, IUnitOfWork unitOfWork, IServiceScopeFactory serviceScopeFactory, ITransactionCategoryRepository categoryRepository, ITransactionRepository transactionRepository, ITransactionSubcategoryRepository subcategoryRepository)
        {
            _accountRepository = accountRepository;
            _transactionEntityRepository = transactionEntityRepository;
            _unitOfWork = unitOfWork;
            _serviceScopeFactory = serviceScopeFactory;
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<Result<Transaction>> Handle(AddOutcomeTransactionCommand request, CancellationToken cancellationToken)
        {
            var sourceAccount = await _accountRepository.GetAccountByIdAsync(new(Guid.Parse(request.SourceAccountId)), cancellationToken);

            if (sourceAccount is null)
            {
                return Result.Failure<Transaction>(Error.NotFound($"Account with ID: {request.SourceAccountId} not found"));
            }

            var recipient = await _transactionEntityRepository.GetByNameIncludeAsync<TransactionRecipient, IEnumerable<Transaction>>(
                new(request.RecipientName),
                te => te.Transactions,
                cancellationToken);

            var category = await GetOrCreateCategory(new (request.CategoryValue), cancellationToken);

            var subcategory = await GetOrCreateSubcategory(new (request.SubcategoryValue), category, cancellationToken);

            category.AddSubcategory(subcategory);

            if (recipient is null)
            {
                var recipientCreateResult = await CreateRecipient(request.RecipientName);

                if (recipientCreateResult.IsFailure)
                {
                    return Result.Failure<Transaction>(recipientCreateResult.Error);
                }

                recipient = recipientCreateResult.Value as TransactionRecipient;
            }

            // TODO fetch currency from user
            var currency = Currency.Usd;

            var createdTransaction = TransactionService.CreateOutgoingTransaction(
                new(request.TransactionAmount, currency),
                sourceAccount,
                recipient!,
                category,
                subcategory,
                request.TransactionDateTime);

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (IsNewRecipient(recipient))
            {
                _transactionEntityRepository.Update(recipient!);
                isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
            }

            if (isSuccessful)
            {
                return Result.Success(createdTransaction);
            }

            return Result.Failure<Transaction>(Error.TaskFailed("Problem while adding outcome transaction"));
        }

        private async Task<TransactionSubcategory> GetOrCreateSubcategory(TransactionSubcategoryValue subcategoryValue, OutcomeTransactionCategory category, CancellationToken cancellationToken)
        {
            return await _subcategoryRepository.GetByValueAsync(
                       subcategoryValue,
                       category,
                       cancellationToken) ??
                   new (subcategoryValue, category);
        }

        private async Task<OutcomeTransactionCategory> GetOrCreateCategory(TransactionCategoryValue categoryValue, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetByValueAsync<OutcomeTransactionCategory>(
                       categoryValue,
                       cancellationToken) ??
                   new(categoryValue);
        }

        private static bool IsNewRecipient(TransactionRecipient? recipient)
        {
            return recipient!.Transactions.Count == 1;
        }

        private async Task<Result<TransactionEntity>> CreateRecipient(string recipientName)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
            var command = new AddTransactionEntityCommand(recipientName, TransactionEntityType.Recipient.Value);
            var recipientCreateResult = await mediator.Send(command);

            return recipientCreateResult;
        }
    }
}
