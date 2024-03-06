import {Money} from "../shared/money";

export interface BudgetedTransactionCategory {
    categoryId: string
    budgetedAmount: Money
    actualAmount: Money
}