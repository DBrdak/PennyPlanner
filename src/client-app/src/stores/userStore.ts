import { makeAutoObservable } from "mobx"
import {RegisterUserCommand} from "../models/requests/users/registerUserCommand";
import {LogInUserCommand} from "../models/requests/users/logInUserCommand";
import agent from "../api/agent";

export default class UserStore {
    currentUser?: User = undefined
    token: string | null = localStorage.getItem('jwt');
    loading: boolean = false

    constructor() {
        makeAutoObservable(this);
    }

    private setLoading(state: boolean) {
        this.loading = state
    }

    private setCurrentUser(user: User) {
        this.currentUser = user
    }

    setToken = (token: string | null) => {
        this.token = token;
    }

    async register(command: RegisterUserCommand) {
        this.setLoading(true)

        try {
            const user = await agent.users.registerUser(command)
            this.setCurrentUser(user)
            await this.logIn({email: command.email, password: command.password})
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }

    async logIn(command: LogInUserCommand) {
        this.setLoading(true)

        try {
            const accessToken = await agent.users.logInUser(command)

            if(accessToken.value.length > 0) {
                localStorage.setItem('jwt', accessToken.value)
                this.setToken(accessToken.value)
                await this.loadCurrentUser()
            }

        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }

    async loadCurrentUser() {
        this.setLoading(true)

        try {
            const user = await agent.users.getCurrentUser()
            this.setCurrentUser(user)
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }
}