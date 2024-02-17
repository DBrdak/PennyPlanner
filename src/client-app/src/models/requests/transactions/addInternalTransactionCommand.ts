export interface AddInternalTransactionCommand {
    fromAccountId: string
    toAccountId: string
    transactionAmount: number
    transactionDateTime: Date
}

export class AddInternalTransactionCommand implements AddInternalTransactionCommand {
    constructor(values?: AddInternalTransactionCommand) {
        this.fromAccountId = values?.fromAccountId || ''
        this.toAccountId = values?.toAccountId || ''
        this.transactionAmount = values?.transactionAmount || 0
        this.transactionDateTime = values?.transactionDateTime || new Date()
    }
}