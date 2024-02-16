export interface AddTransactionCategoryCommand {
    value: string
    type: string
}

export class AddTransactionCategoryCommand implements AddTransactionCategoryCommand {
    constructor(type: 'income' | 'outcome') {
        this.value = ''
        this.type = type
    }
}