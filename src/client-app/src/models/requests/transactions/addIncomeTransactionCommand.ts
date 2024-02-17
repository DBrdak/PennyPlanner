export interface AddIncomeTransactionCommand {
    destinationAccountId: string
    senderName: string
    transactionAmount: number
    categoryValue: string
    subcategoryValue: string
    transactionDateTime: Date
}

export class AddIncomeTransactionCommand implements AddIncomeTransactionCommand {
    constructor(values?: AddIncomeTransactionCommand) {
        this.destinationAccountId = values?.destinationAccountId || ''
        this.senderName = values?.senderName || ''
        this.transactionAmount = values?.transactionAmount || 0
        this.categoryValue = values?.categoryValue || ''
        this.subcategoryValue = values?.categoryValue || ''
        this.transactionDateTime = values?.transactionDateTime || new Date()
    }
}