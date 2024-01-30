import {Money} from "../shared/money";

export interface NewAccountData {
    type: string
    name: string
    initialBalance: Money
}