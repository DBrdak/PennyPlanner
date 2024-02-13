
export interface AddOutcomeTransactionCommand {
    sourceAccountId: string
    recipientName: string
    transactionAmount: number
    category: string
    transactionDateTime: Date
}

export class AddOutcomeTransactionCommand implements AddOutcomeTransactionCommand {
    constructor() {
        this.sourceAccountId = ''
        this.recipientName = ''
        this.transactionAmount = 0
        this.category = ''
        this.transactionDateTime = new Date()
    }
}