import {Money} from "../shared/money";

export interface Transaction {
    accountId?: string | null
    fromAccountId?: string | null
    toAccountId?: string | null
    senderId?: string | null
    recipientId?: string | null
    transactionAmount: Money
    category: string
    transactionDateUtc: Date
}