import {makeAutoObservable, runInAction} from "mobx";
import {TransactionEntity} from "../models/transactionEntities/transactionEntity";
import agent from "../api/agent";

export default class TransactionEntityStore {
    private transactionEntitiesRegistry: Map<string, TransactionEntity> = new Map<string, TransactionEntity>()
    loading: boolean = false

    constructor() {
        makeAutoObservable(this);
    }

    get transactionEntities() {
        return Array.from(this.transactionEntitiesRegistry.values())
    }

    getTransactionEntity(id: string) {
        return this.transactionEntitiesRegistry.get(id)
    }

    private setLoading(state: boolean){
        this.loading = state
    }

    private setTransactionEntity(transactionEntity: TransactionEntity){
        this.transactionEntitiesRegistry.set(transactionEntity.transactionEntityId, transactionEntity)
    }

    async loadTransactionEntities() {
        this.setLoading(true);
        try {
            const transactionEntities = await agent.transactionEntities.getTransactionEntities();
            runInAction(() => {
                this.transactionEntitiesRegistry.clear();
                transactionEntities.forEach((te) => this.setTransactionEntity(te));
            });
        } catch (e) {
            console.error(e);
        } finally {
            this.setLoading(false);
        }
    }
}