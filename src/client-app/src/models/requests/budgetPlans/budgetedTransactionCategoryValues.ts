import {TransactionCategory} from "../../transactionCategories/transactionCategory";

export interface BudgetedTransactionCategoryValues {
    categoryValue: string
    categoryType: string
    budgetedAmount: number
}

export class BudgetedTransactionCategoryValues implements BudgetedTransactionCategoryValues {
    constructor(category: TransactionCategory) {
        this.categoryType = category.type
        this.categoryValue = category.value
        this.budgetedAmount = 0
    }
}