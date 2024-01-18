import {Money} from "../shared/money";
import {Transaction} from "../transactions/transaction";

export interface Account {
    accountId: string
    name: string
    balance: Money
    transactions: Transaction[]
    accountType: string
}