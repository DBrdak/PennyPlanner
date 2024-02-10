import {Money} from "../shared/money";
import {TransactionCategory} from "../transactionCategories/transactionCategory";

export interface BudgetedTransactionCategory {
    category: TransactionCategory
    budgetedAmount: Money
    actualAmount: Money
}