import {BudgetedTransactionCategoryValues} from "./budgetedTransactionCategoryValues";

export interface SetBudgetPlanCommand {
    budgetPlanForDate: Date
    budgetedTransactionCategoryValues: BudgetedTransactionCategoryValues[]
}