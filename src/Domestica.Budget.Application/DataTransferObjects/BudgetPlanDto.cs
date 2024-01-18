﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DateKit.DB;
using Domestica.Budget.Domain.BudgetPlans;
using Domestica.Budget.Domain.Transactions;

namespace Domestica.Budget.Application.DataTransferObjects
{
    public sealed record BudgetPlanDto
    {
        public DateTimeRange BudgetPeriod { get; init; }
        public IEnumerable<BudgetedTransactionCategoryDto> BudgetedTransactionCategories { get; init; }
        public IEnumerable<TransactionDto> Transactions { get; init; }

        private BudgetPlanDto(
            DateTimeRange BudgetPeriod,
            IEnumerable<BudgetedTransactionCategoryDto> BudgetedTransactionCategories,
            IEnumerable<TransactionDto> Transactions)
        {
            this.BudgetPeriod = BudgetPeriod;
            this.BudgetedTransactionCategories = BudgetedTransactionCategories;
            this.Transactions = Transactions;
        }

        internal static BudgetPlanDto FromDomainObject(BudgetPlan domainObject)
        {
            var transactions = domainObject.Transactions.Select(TransactionDto.FromDomainObject);
            var budgetedTransactionCategories =
                domainObject.BudgetedTransactionCategories.Select(BudgetedTransactionCategoryDto.FromDomainObject);

            return new (domainObject.BudgetPeriod, budgetedTransactionCategories, transactions);
        }
    }
}
