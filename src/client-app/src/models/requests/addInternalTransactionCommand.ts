
export interface AddInternalTransactionCommand {
    fromAccountId: string | null
    toAccountId: string | null
    transactionAmount: number
    transactionDateTime: Date
}