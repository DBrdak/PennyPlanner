
export interface AddInternalTransactionCommand {
    fromAccountId: string
    toAccountId: string
    transactionAmount: number
    transactionDateTime: Date
}

export class AddInternalTransactionCommand implements AddInternalTransactionCommand {
    constructor() {
        this.fromAccountId = ''
        this.toAccountId = ''
        this.transactionAmount = 0
        this.transactionDateTime = new Date()
    }
}