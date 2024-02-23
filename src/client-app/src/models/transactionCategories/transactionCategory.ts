import {TransactionSubcategory} from "../transactionSubcategories/transactionSubcategory";

export interface TransactionCategory {
    transactionCategoryId: string
    value: string
    type: string
    subcategories: TransactionSubcategory[]
}