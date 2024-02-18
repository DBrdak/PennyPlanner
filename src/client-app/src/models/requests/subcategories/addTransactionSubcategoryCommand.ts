export interface AddTransactionSubcategoryCommand {
    value: string;
    categoryId: string;
}

export class AddTransactionSubcategoryCommand implements AddTransactionSubcategoryCommand {
    constructor(categoryId: string) {
        this.value = ''
        this.categoryId = categoryId
    }
}