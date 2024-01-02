﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budgetify.Domain.Shared.TransactionCategories;
using CommonAbstractions.DB.Entities;
using DateKit.DB;
#pragma warning disable CS8618

namespace Budgetify.Domain.BudgetPlans
{
    public sealed class BudgetPlan : Entity
    {
        public DateTimeRange BudgetPeriod { get; private set; }
        public ImmutableList<BudgetedTransactionCategory> BudgetedTransactionCategories => _budgetedTransactionCategories.ToImmutableList();
        private readonly List<BudgetedTransactionCategory> _budgetedTransactionCategories;

        private BudgetPlan()
        { }

        public BudgetPlan(DateTimeRange budgetPeriod)
        {
            _budgetedTransactionCategories = new ();
            BudgetPeriod = budgetPeriod;
        }
    }
}
