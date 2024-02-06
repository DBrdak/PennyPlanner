
export interface AddOutcomeTransactionCommand {
    sourceAccountId: string | null
    recipientId: string | null
    transactionAmount: number
    category: string | null
    transactionDateTime: Date
}