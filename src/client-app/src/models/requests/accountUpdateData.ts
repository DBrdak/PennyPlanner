import {Account} from "../accounts/account";

export interface AccountUpdateData {
    accountId: string
    name: string
    balance: number
}

export class AccountUpdateData implements AccountUpdateData {
    constructor(account: Account) {
        this.name = account.name
        this.balance = account.balance.amount
        this.accountId = account.accountId
    }
}