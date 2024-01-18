import {Money} from "../shared/money";

export interface AddInternalTransactionCommand {
    fromAccountId: string | null
    toAccountId: string | null
    transactionAmount: Money
}