import { makeAutoObservable, runInAction } from "mobx";
import agent from "../api/agent";
import {AddTransactionSubcategoryCommand} from "../models/requests/subcategories/addTransactionSubcategoryCommand";

export default class TransactionSubcategoryStore {
    loading: boolean = false;

    constructor() {
        makeAutoObservable(this);
    }

    private setLoading(state: boolean) {
        this.loading = state;
    }

    async addTransactionSubcategory(command: AddTransactionSubcategoryCommand) {
        this.setLoading(true);
        try {
            await agent.transactionSubcategories.addTransactionSubcategory(command)
        } catch (e) {
            console.error(e);
        } finally {
            this.setLoading(false);
        }
    }

    async updateTransactionSubcategory(id: string, newValue: string) {
        this.setLoading(true);
        try {
            await agent.transactionSubcategories.updateTransactionSubcategory(id, newValue)
        } catch (e) {
            console.error(e);
        } finally {
            this.setLoading(false);
        }
    }

    async deleteTransactionSubcategory(subcategoryId: string) {
        this.setLoading(true);
        try {
            await agent.transactionSubcategories.removeTransactionSubcategory(subcategoryId)
        } catch (e) {
            console.error(e);
        } finally {
            this.setLoading(false);
        }
    }
}
