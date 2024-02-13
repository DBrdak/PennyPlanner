using CommonAbstractions.DB;
using CommonAbstractions.DB.Messaging;
using Domestica.Budget.Application.TransactionCategories.AddTransactionCategory;
using Domestica.Budget.Application.TransactionEntities.AddTransactionEntity;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.TransactionCategories;
using Domestica.Budget.Domain.TransactionEntities;
using Domestica.Budget.Domain.TransactionEntities.TransactionSenders;
using Domestica.Budget.Domain.Transactions;
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
        private readonly IUnitOfWork _unitOfWork;

        public AddIncomeTransactionCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork, ITransactionEntityRepository transactionEntityRepository, IServiceScopeFactory serviceScopeFactory, ITransactionCategoryRepository categoryRepository)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _transactionEntityRepository = transactionEntityRepository;
            _serviceScopeFactory = serviceScopeFactory;
            _categoryRepository = categoryRepository;
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
                cancellationToken) as TransactionSender;

            var category = await _categoryRepository.GetByValueAsync<IncomeTransactionCategory>(
                new(request.CategoryValue),
                cancellationToken) ?? new(new(request.CategoryValue));

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
                category!, 
                request.TransactionDateTime);

            var isSuccessful = await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;

            if (isSuccessful)
            {
                return Result.Success(createdTransaction);
            }

            return Result.Failure<Transaction>(Error.TaskFailed("Problem while adding income transaction"));
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
