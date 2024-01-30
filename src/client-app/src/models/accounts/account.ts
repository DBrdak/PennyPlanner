import {Money} from "../shared/money";
import {Transaction} from "../transactions/transaction";

export interface Account {
    accountId: string
    name: string
    balance: Money
    transactions: Transaction[]
    accountType: string
}

export class Account implements Account{
    constructor(
        public accountId: string,
        public name: string,
        public balance: Money,
        public transactions: Transaction[],
        public accountType: string
    ) {
        this.accountId = accountId
        this.name = name
        this.balance = balance
        this.transactions = transactions
        this.accountType = accountType
    }
}