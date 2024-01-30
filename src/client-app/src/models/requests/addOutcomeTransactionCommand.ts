import {Money} from "../shared/money";

export interface AddOutcomeTransactionCommand {
    sourceAccountId: string | null
    recipientId: string | null
    transactionAmount: Money
    category: string | null
}