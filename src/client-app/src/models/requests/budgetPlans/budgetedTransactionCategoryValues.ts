import {TransactionCategory} from "../../transactionCategories/transactionCategory";

export interface BudgetedTransactionCategoryValues {
    categoryValue: string
    categoryType: string
    budgetedAmount: number
}

export class BudgetedTransactionCategoryValues implements BudgetedTransactionCategoryValues {
    constructor(category?: TransactionCategory, categoryType?: 'income' | 'outcome') {

        if(category) {
            this.categoryType = category.type
            this.categoryValue = category.value
            this.budgetedAmount = 0
        } else if (categoryType && !category) {
            this.categoryType = categoryType
            this.categoryValue = ''
            this.budgetedAmount = 0
        } else {
            this.categoryType = ''
            this.categoryValue = ''
            this.budgetedAmount = 0
        }
    }
}