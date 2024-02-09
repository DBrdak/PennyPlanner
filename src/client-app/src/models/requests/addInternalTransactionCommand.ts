
export interface AddInternalTransactionCommand {
    fromAccountId: string
    toAccountId: string
    transactionAmount: number
    transactionDateTime: Date
}