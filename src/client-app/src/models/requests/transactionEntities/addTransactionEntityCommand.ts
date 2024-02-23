export interface AddTransactionEntityCommand {
    name: string
    type: string
}

export class AddTransactionEntityCommand implements AddTransactionEntityCommand {
    constructor(type: 'sender' | 'recipient') {
        this.name = ''
        this.type = type
    }
}