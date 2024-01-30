import {Money} from "../shared/money";

export interface AddIncomeTransactionCommand {
    destinationAccountId: string | null
    senderId: string | null
    transactionAmount: Money
    category: string | null
}