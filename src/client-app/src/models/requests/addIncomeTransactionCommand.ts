import {Money} from "../shared/money";

export interface AddIncomeTransactionCommand {
    destinationAccountId: string | null
    senderId: string | null
    transactionAmount: number
    category: string | null
    transactionDateTime: Date
}