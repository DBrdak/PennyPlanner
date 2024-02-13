import {TransactionCategory} from "../transactionCategories/transactionCategory";
import {TransactionEntity} from "../transactionEntities/transactionEntity";
import {Account} from "../accounts/account";

export interface AddIncomeTransactionCommand {
    destinationAccountId: string
    senderName: string
    transactionAmount: number
    category: string
    transactionDateTime: Date
}

export class AddIncomeTransactionCommand implements AddIncomeTransactionCommand {
    constructor() {
        this.destinationAccountId = ''
        this.senderName = ''
        this.transactionAmount = 0
        this.category = ''
        this.transactionDateTime = new Date()
    }
}