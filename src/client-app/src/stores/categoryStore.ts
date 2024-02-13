import { makeAutoObservable } from "mobx"
import {TransactionCategory} from "../models/transactionCategories/transactionCategory";
import agent from "../api/agent";

export default class CategoryStore {
    private categoriresRegistry: Map<string, TransactionCategory> = new Map<string, TransactionCategory>()
    loading: boolean = false

    constructor() {
        makeAutoObservable(this);
    }

    get categories() {
        return Array.from(this.categoriresRegistry.values())
    }

    private setLoading(state: boolean) {
        this.loading = state
    }

    private setCategory(category: TransactionCategory) {
        this.categoriresRegistry.set(category.transactionCategoryId, category)
    }

    async loadCategories() {
        this.setLoading(true)
        try {
            const categories = await agent.transactionCategories.getTransactionCategories()
            categories.forEach(c => this.setCategory(c))
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }
}