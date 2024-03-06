using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB;
using CommonAbstractions.DB.Entities;
using Domestica.Budget.Domain.Accounts;
using Domestica.Budget.Domain.BudgetPlans;
using Domestica.Budget.Domain.Transactions.DomainEvents;

namespace Domestica.Budget.Application.Transactions.DomainEventHandlers
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

            if (budgetPlan is null)
            {
                return;
            }

            if (account is null)
            {
                return;
            }

            budgetPlan.RemoveTransaction(notification.DeletedTransaction);
            account.RemoveTransaction(notification.DeletedTransaction);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
