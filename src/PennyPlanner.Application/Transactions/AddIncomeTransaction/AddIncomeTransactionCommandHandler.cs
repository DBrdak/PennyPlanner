using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Money.DB;
using PennyPlanner.Application.Abstractions.Authentication;
using PennyPlanner.Application.TransactionEntities.AddTransactionEntity;
using PennyPlanner.Domain.Accounts;
using PennyPlanner.Domain.TransactionCategories;
using PennyPlanner.Domain.TransactionEntities;
using PennyPlanner.Domain.TransactionEntities.TransactionSenders;
using PennyPlanner.Domain.Transactions;
using PennyPlanner.Domain.TransactionSubcategories;
using PennyPlanner.Domain.Users;
using Responses.DB;

namespace PennyPlanner.Application.Transactions.AddIncomeTransaction
{
    internal sealed class AddIncomeTransactionCommandHandler : ICommandHandler<AddIncomeTransactionCommand, Transaction>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionEntityRepository _transactionEntityRepository;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ITransactionCategoryRepository _categoryRepository;
        private readonly ITransactionSubcategoryRepository _subcategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;

        public AddIncomeTransactionCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork, ITransactionEntityRepository transactionEntityRepository, IServiceScopeFactory serviceScopeFactory, ITransactionCategoryRepository categoryRepository, ITransactionSubcategoryRepository subcategoryRepository, IUserContext userContext)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _transactionEntityRepository = transactionEntityRepository;
            _serviceScopeFactory = serviceScopeFactory;
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
            _userContext = userContext;
        }

        public async Task<Result<Transaction>> Handle(AddIncomeTransactionCommand request, CancellationToken cancellationToken)
        {
            var destinationAccount = await _accountRepository.GetAccountByIdAsync(new(Guid.Parse(request.DestinationAccountId)), cancellationToken);

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

            var currency = Currency.FromCode(_userContext.UserCurrencyCode);

            var createdTransaction = TransactionService.CreateIncomingTransaction(
                new (request.TransactionAmount, currency),
                destinationAccount,
                sender!,
                category,
                subcategory,
                request.TransactionDateTime);
            

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (IsNewSender(sender!))
            {
                _transactionEntityRepository.Update(sender!);
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
                   new(subcategoryValue, category, new UserId(_userContext.IdentityId));
        }

        private async Task<IncomeTransactionCategory> GetOrCreateCategory(TransactionCategoryValue categoryValue, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetByValueAsync<IncomeTransactionCategory>(
                       categoryValue,
                       cancellationToken) ??
                   new(categoryValue, new UserId(_userContext.IdentityId));
        }

        private static bool IsNewSender(TransactionSender sender)
        {
            return sender.Transactions.Count == 1;
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
