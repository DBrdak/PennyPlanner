import {BudgetedTransactionCategory} from "./budgetedTransactionCategory";
import {Transaction} from "../transactions/transaction";
import {DateTimeRange} from "../shared/dateTimeRange";

export interface BudgetPlan {
    budgetPlanId: string
    budgetPeriod: DateTimeRange
    budgetedTransactionCategories: BudgetedTransactionCategory[]
    transactions: Transaction[]
}