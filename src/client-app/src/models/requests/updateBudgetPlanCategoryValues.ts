import {Money} from "../shared/money";

export interface UpdateBudgetPlanCategoryValues {
    newBudgetAmount?: Money | null
    isBudgetToReset: boolean
}