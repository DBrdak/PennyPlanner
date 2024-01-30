import {BudgetedTransactionCategory} from "./budgetedTransactionCategory";
import {Transaction} from "../transactions/transaction";

export interface BudgetPlan {
    budgetPeriod: DateTimeRange
    budgetedTransactionCategories: BudgetedTransactionCategory[]
    transactions: Transaction[]
}