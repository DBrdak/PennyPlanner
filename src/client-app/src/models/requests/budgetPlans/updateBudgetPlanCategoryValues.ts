export interface UpdateBudgetPlanCategoryValues {
    newBudgetAmount?: number
    isBudgetToReset: boolean
}

export class UpdateBudgetPlanCategoryValues implements UpdateBudgetPlanCategoryValues {
    constructor(newBudgetAmount?: number) {
        this.isBudgetToReset = !newBudgetAmount
        this.newBudgetAmount = newBudgetAmount
    }
}