import {makeAutoObservable, runInAction} from "mobx";
import {Account} from "../models/accounts/account";
import agent from "../api/agent";
import {NewAccountData} from "../models/requests/newAccountData";
import {AccountUpdateData} from "../models/requests/accountUpdateData";

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

    private removeAccount(accountId: string) {
        this.accountsRegistry.delete(accountId)
    }

    private replaceAccount(newAccountData: Account) {
        this.removeAccount(newAccountData.accountId)
        this.setAccount(newAccountData)
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

    async addAccount(accountData: NewAccountData) {
        this.setLoading(true);
        try {
            const account = await agent.accounts.createAccount(accountData)
            this.accountsRegistry.clear()
            return account
        } catch (e) {
            console.error(e)
        } finally {
            this.setLoading(false)
        }
    }

    async updateAccount(accountData: AccountUpdateData) {
        this.setLoading(true);
        try {
            const account =await agent.accounts.updateAccount(accountData)
            this.replaceAccount(account)
            return account
        } catch (e) {
            console.error(e);
        } finally {
            this.setLoading(false);
        }
    }

    getAccount(accountId: string) {
        return this.accountsRegistry.get(accountId)
    }
}