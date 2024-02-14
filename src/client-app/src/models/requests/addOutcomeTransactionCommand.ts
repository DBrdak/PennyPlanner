
export interface AddOutcomeTransactionCommand {
    sourceAccountId: string
    recipientName: string
    transactionAmount: number
    categoryValue: string
    transactionDateTime: Date
}

export class AddOutcomeTransactionCommand implements AddOutcomeTransactionCommand {
    constructor(values?: AddOutcomeTransactionCommand) {
        this.sourceAccountId = values?.sourceAccountId || ''
        this.recipientName = values?.recipientName || ''
        this.transactionAmount = values?.transactionAmount || 0
        this.categoryValue = values?.categoryValue || ''
        this.transactionDateTime = values?.transactionDateTime || new Date()
    }
}