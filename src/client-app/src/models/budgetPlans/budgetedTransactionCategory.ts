import {Money} from "../shared/money";
import {TransactionCategory} from "../transactionCategories/transactionCategory";

export interface BudgetedTransactionCategory {
    categoryId: string
    budgetedAmount: Money
    actualAmount: Money
}