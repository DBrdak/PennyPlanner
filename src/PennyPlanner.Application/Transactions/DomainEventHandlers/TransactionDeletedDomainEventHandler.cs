﻿using CommonAbstractions.DB;
using CommonAbstractions.DB.Entities;
using PennyPlanner.Domain.Accounts;
using PennyPlanner.Domain.BudgetPlans;
using PennyPlanner.Domain.Transactions.DomainEvents;

namespace PennyPlanner.Application.Transactions.DomainEventHandlers
{
    internal sealed class TransactionDeletedDomainEventHandler : IDomainEventHandler<TransactionDeletedDomainEvent>
    {
        private readonly IBudgetPlanRepository _budgetPlanRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionDeletedDomainEventHandler(IBudgetPlanRepository budgetPlanRepository, IUnitOfWork unitOfWork, IAccountRepository accountRepository)
        {
            _budgetPlanRepository = budgetPlanRepository;
            _unitOfWork = unitOfWork;
            _accountRepository = accountRepository;
        }

        public async Task Handle(TransactionDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            var budgetPlan = await _budgetPlanRepository.GetBudgetPlanByDateAsync(
                notification.DeletedTransaction.TransactionDateUtc,
                cancellationToken);

            var account = await _accountRepository.GetAccountByIdAsync(
                notification.DeletedTransaction.AccountId,
                cancellationToken);


            budgetPlan?.RemoveTransaction(notification.DeletedTransaction);

            account?.RemoveTransaction(notification.DeletedTransaction);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
