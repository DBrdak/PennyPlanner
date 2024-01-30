import {Money} from "../shared/money";

export interface UpdateBudgetPlanCategoryValues {
    newBudgetAmount?: number | null
    isBudgetToReset: boolean
}