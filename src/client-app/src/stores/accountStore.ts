import {makeAutoObservable, runInAction} from "mobx";
import {Account} from "../models/accounts/account";
import agent from "../api/agent";

export default class AccountStore {
    private accountsRegistry: Map<string, Account> = new Map<string, Account>()
    loading: boolean = false

    constructor() {
        makeAutoObservable(this);
    }

    get accounts() {
        return Array.from(this.accountsRegistry.values())
    }

    private setLoading(state: boolean){
        this.loading = state
    }

    private setAccount(account: Account){
        this.accountsRegistry.set(account.accountId, account)
    }

    async loadAccounts() {
        this.setLoading(true);
        try {
            const accounts = await agent.accounts.getAccounts();
            runInAction(() => {
                this.accountsRegistry.clear();
                accounts.forEach((a) => this.setAccount(a));
            });
        } catch (e) {
            console.error(e);
        } finally {
            this.setLoading(false);
        }
    }
}