import {makeAutoObservable, runInAction} from "mobx"
import {TransactionCategory} from "../models/transactionCategories/transactionCategory";
import agent from "../api/agent";
import {AddTransactionCategoryCommand} from "../models/requests/categories/addTransactionCategoryCommand";

export default class CategoryStore {
    private categoriesRegistry: Map<string, TransactionCategory> = new Map<string, TransactionCategory>()
    private selectedCategoryId?: string
    loading: boolean = false

    constructor() {
        makeAutoObservable(this);
    }

    get categories() {
        return Array.from(this.categoriesRegistry.values())
    }

    get selectedCategory() {
        return this.selectedCategoryId ?
            this.categoriesRegistry.get(this.selectedCategoryId)
            : undefined
    }

    private setLoading(state: boolean) {
        this.loading = state
    }

    private setCategory(category: TransactionCategory) {
        this.categoriesRegistry.set(category.transactionCategoryId, category)
    }

    setSelectedCategory(categoryId: string | undefined) {
        this.selectedCategoryId = categoryId
    }

    async loadCategories() {
        this.setLoading(true)
        try {
            const categories = await agent.transactionCategories.getTransactionCategories()
            runInAction(() => {
                this.categoriesRegistry.clear();
                categories.forEach(c => this.setCategory(c))
            });
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }

    async addTransactionCategory(command: AddTransactionCategoryCommand) {
        this.setLoading(true);
        try {
            await agent.transactionCategories.createTransactionCategory(command).then(async () => {
                await this.loadCategories()
            })
        } catch (e) {
            console.error(e);
        } finally {
            this.setLoading(false);
        }
    }

    async deleteTransactionCategory(id: string) {
        this.setLoading(true);
        try {
            await agent.transactionCategories.deleteTransactionCategory(id).then(async () => {
                await this.loadCategories()
            })
        } catch (e) {
            console.error(e);
        } finally {
            this.setLoading(false);
        }
    }

    async updateTransactionCategory(id: string, newName: string) {
        this.setLoading(true);
        try {
            await agent.transactionCategories.updateTransactionCategory(id, newName).then(async () => {
                await this.loadCategories()
            })
        } catch (e) {
            console.error(e);
        } finally {
            this.setLoading(false);
        }
    }

    getCategory(categoryId: string) {
        return this.categoriesRegistry.get(categoryId)
    }
}