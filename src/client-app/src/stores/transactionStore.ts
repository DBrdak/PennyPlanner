import {makeAutoObservable} from "mobx";
import agent from "../api/agent";
import {Transaction} from "../models/transactions/transaction";
import {store} from "./store";
import {AddIncomeTransactionCommand} from "../models/requests/addIncomeTransactionCommand";
import {AddOutcomeTransactionCommand} from "../models/requests/addOutcomeTransactionCommand";
import {AddInternalTransactionCommand} from "../models/requests/addInternalTransactionCommand";
import {toast} from "react-toastify";

export default class TransactionStore {
    private transactionsRegistry: Map<string, Transaction> = new Map<string, Transaction>()
    transactionsIdToRemove: string[] = []
    loading: boolean = false

    constructor() {
        makeAutoObservable(this);
    }

    get transactions() {
        return Array.from(this.transactionsRegistry.values())
    }

    private setLoading(state: boolean) {
        this.loading = state
    }

    private setTransactionsIdToRemove(ids: string[]) {
        this.transactionsIdToRemove = ids
    }

    private setTransaction(transaction: Transaction) {
        this.transactionsRegistry.set(transaction.transactionId, transaction)
    }

    setTransactionIdToRemove(id: string) {
        this.transactionsIdToRemove.push(id)
    }

    removeTransactionIdToRemove(id: string) {
        this.setTransactionsIdToRemove([...this.transactionsIdToRemove.filter(tid => tid !== id)])
    }


    clearTransactionIdToRemove() {
        for (const id of this.transactionsIdToRemove) {
            this.removeTransactionIdToRemove(id)
        }
    }

    async loadTransactions() {
        this.setLoading(true)
        try {
            let transactions = store.accountStore.accounts.length > 0 &&
                store.accountStore.accounts.flatMap(a => a.transactions)

            if(!transactions) {
                transactions = await agent.transactions.getTransactions()
            }

            this.transactionsRegistry.clear()
            transactions.forEach(t => this.setTransaction(t))
        } catch (e){
            console.log(e)
        } finally {
            this.setLoading(false)
        }
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

    async addTransaction(command: AddIncomeTransactionCommand | AddOutcomeTransactionCommand | AddInternalTransactionCommand) {
        this.setLoading(true)
        try {
            if (command instanceof AddIncomeTransactionCommand) {
                await agent.transactions.createIncomeTransaction(command as AddIncomeTransactionCommand)
            } else if (command instanceof AddOutcomeTransactionCommand) {
                await agent.transactions.createOutcomeTransaction(command as AddOutcomeTransactionCommand)
            } else if (command instanceof AddInternalTransactionCommand) {
                await agent.transactions.createInternalTransaction(command as AddInternalTransactionCommand)
            } else {
                toast.error('Invalid transaction type')
            }
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }
}