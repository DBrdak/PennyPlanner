using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.TransactionCategories.AddTransactionCategory;
using Domestica.Budget.Application.TransactionEntities.AddTransactionEntity;
using Domestica.Budget.Application.Transactions.AddOutcomeTransaction;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.TransactionEntities.TransactionSenders;
using Domestica.Budget.Domain.Transactions;
using Domestica.Budget.Domain.TransactionSubcategories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Money.DB;
using Responses.DB;

namespace Domestica.Budget.Application.Transactions.AddIncomeTransaction
{
    internal sealed class AddIncomeTransactionCommandHandler : ICommandHandler<AddIncomeTransactionCommand, Transaction>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionEntityRepository _transactionEntityRepository;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ITransactionCategoryRepository _categoryRepository;
        private readonly ITransactionSubcategoryRepository _subcategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddIncomeTransactionCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork, ITransactionEntityRepository transactionEntityRepository, IServiceScopeFactory serviceScopeFactory, ITransactionCategoryRepository categoryRepository, ITransactionSubcategoryRepository subcategoryRepository)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _transactionEntityRepository = transactionEntityRepository;
            _serviceScopeFactory = serviceScopeFactory;
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<Result<Transaction>> Handle(AddIncomeTransactionCommand request, CancellationToken cancellationToken)
        {
            var destinationAccount = await _accountRepository.GetUserAccountByIdAsync(new(Guid.Parse(request.DestinationAccountId)), cancellationToken);

            if (destinationAccount is null)
            {
                return Result.Failure<Transaction>(Error.NotFound($"Account with ID: {request.DestinationAccountId} not found"));
            }

            var sender = await _transactionEntityRepository.GetByNameIncludeAsync<TransactionSender, IEnumerable<Transaction>>(
                new(request.SenderName),
                s => s.Transactions,
                cancellationToken);

            var category = await GetOrCreateCategory(new(request.CategoryValue), cancellationToken);

            var subcategory = await GetOrCreateSubcategory(new(request.SubcategoryValue), category, cancellationToken);

            category.AddSubcategory(subcategory);

            if (sender is null)
            {
                var senderCreateResult = await CreateSender(request.SenderName);

                if (senderCreateResult.IsFailure)
                {
                    return Result.Failure<Transaction>(senderCreateResult.Error);
                }

                sender = senderCreateResult.Value as TransactionSender;
            }

            // TODO fetch currency from user
            var currency = Currency.Usd;

            var createdTransaction = TransactionService.CreateIncomingTransaction(
                new (request.TransactionAmount, currency),
                destinationAccount,
                sender!,
                category,
                subcategory,
                request.TransactionDateTime);

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (IsNewSender(sender))
            {
                _transactionEntityRepository.Update(sender);
                isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
            }

            if (isSuccessful)
            {
                return Result.Success(createdTransaction);
            }

            return Result.Failure<Transaction>(Error.TaskFailed("Problem while adding income transaction"));
        }

        private async Task<TransactionSubcategory> GetOrCreateSubcategory(TransactionSubcategoryValue subcategoryValue, IncomeTransactionCategory category, CancellationToken cancellationToken)
        {
            return await _subcategoryRepository.GetByValueAsync(
                       subcategoryValue,
                       category,
                       cancellationToken) ??
                   new(subcategoryValue, category);
        }

        private async Task<IncomeTransactionCategory> GetOrCreateCategory(TransactionCategoryValue categoryValue, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetByValueAsync<IncomeTransactionCategory>(
                       categoryValue,
                       cancellationToken) ??
                   new(categoryValue);
        }

        private static bool IsNewSender(TransactionSender? sender)
        {
            return sender!.Transactions.Count == 1;
        }

        private async Task<Result<TransactionEntity>> CreateSender(string senderName)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
            var command = new AddTransactionEntityCommand(senderName, TransactionEntityType.Sender.Value);
            var senderCreateResult = await mediator.Send(command);

            return senderCreateResult;
        }
    }
}
