export interface Money {
    amount: number
    currency: string
}

export class Money implements Money {
    constructor(amount: number, currency: string) {
        this.amount = amount
        this.currency = currency
    }
}