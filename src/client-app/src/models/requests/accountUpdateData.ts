import {Money} from "../shared/money";

export interface AccountUpdateData {
    accountId: string | null
    name: string | null
    balance: number
}