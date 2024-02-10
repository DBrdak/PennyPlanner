import {Money} from "../shared/money";
import {TransactionCategory} from "../transactionCategories/transactionCategory";

export interface Transaction {
    transactionId: string
    accountId?: string | null
    fromAccountId?: string | null
    toAccountId?: string | null
    senderId?: string | null
    recipientId?: string | null
    transactionAmount: Money
    category: TransactionCategory
    transactionDateUtc: Date
}