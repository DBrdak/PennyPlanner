
export interface AddOutcomeTransactionCommand {
    sourceAccountId: string
    recipientName: string
    transactionAmount: number
    category: string
    transactionDateTime: Date
}