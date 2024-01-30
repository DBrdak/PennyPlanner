import {Money} from "../shared/money";

export interface NewAccountData {
    type: string
    name: string
    initialBalance: number
}

export class NewAccountData implements NewAccountData{
    constructor() {
        this.type = ''
        this.name = ''
        this.initialBalance = 0
    }
}