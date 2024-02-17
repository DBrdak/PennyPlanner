import {makeAutoObservable, runInAction} from "mobx";
import {Account} from "../models/accounts/account";
import agent from "../api/agent";
import {NewAccountData} from "../models/requests/accounts/newAccountData";
import {AccountUpdateData} from "../models/requests/accounts/accountUpdateData";

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
            await agent.accounts.updateAccount(accountData)
            await this.loadAccounts()
            return this.getAccount(accountData.accountId)
        } catch (e) {
            console.error(e);
        } finally {
            this.setLoading(false);
        }
    }

    async deleteAccount(accountId: string) {
        this.setLoading(true)
        try {
            await agent.accounts.deleteAccount(accountId)
            this.removeAccount(accountId)
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }

    getAccount(accountId: string) {
        return this.accountsRegistry.get(accountId)
    }
}