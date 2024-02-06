import {makeAutoObservable} from "mobx";
import agent from "../api/agent";

export default class TransactionStore {
    transactionsIdToRemove: string[] = []
    loading: boolean = false

    constructor() {
        makeAutoObservable(this);
    }

    private setLoading(state: boolean) {
        this.loading = state
    }

    private setTransactionsIdToRemove(ids: string[]) {
        this.transactionsIdToRemove = ids
    }

    setTransactionIdToRemove(id: string) {
        this.transactionsIdToRemove.push(id)
    }

    removeTransactionIdToRemove(id: string) {
        this.setTransactionsIdToRemove([...this.transactionsIdToRemove.filter(tid => tid !== id)])
    }

    async removeTransactions() {
        this.setLoading(true)
        try {
            for (const id of this.transactionsIdToRemove) {
                console.log(id)
                await agent.transactions.deleteTransaction(id)
                this.removeTransactionIdToRemove(id)
            }
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }

    clearTransactionIdToRemove() {
        for (const id of this.transactionsIdToRemove) {
            this.removeTransactionIdToRemove(id)
        }
    }
}