import {Transaction} from "../transactions/transaction";

export interface TransactionEntity {
    transactionEntityId: string
    name: string
    transactionEntityType: string
    transactions: Transaction[]
}