import {Money} from "../shared/money";

export interface BudgetedTransactionCategory {
    category: string
    budgetedAmount: Money
    actualAmount: Money
}