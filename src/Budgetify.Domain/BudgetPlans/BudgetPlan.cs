﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonAbstractions.DB.Entities;
using DateKit.DB;
#pragma warning disable CS8618

namespace Budgetify.Domain.BudgetPlans
{
    public sealed class BudgetPlan : Entity<BudgetPlanId>
    {
        public DateTimeRange BudgetPeriod { get; private set; }
        public IReadOnlyCollection<BudgetedTransactionCategory> BudgetedTransactionCategories => _budgetedTransactionCategories;
        private readonly List<BudgetedTransactionCategory> _budgetedTransactionCategories;

        private BudgetPlan()
        { }

        public BudgetPlan(DateTimeRange budgetPeriod): base(new BudgetPlanId())
        {
            _budgetedTransactionCategories = new ();
            BudgetPeriod = budgetPeriod;
        }
    }
}
