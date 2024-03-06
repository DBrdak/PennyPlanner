export interface RegisterUserCommand {
    email: string
    username: string
    password: string
    currency: string
}

export class RegisterUserCommand implements RegisterUserCommand {
    constructor() {
        this.email = ''
        this.username = ''
        this.password = ''
        this.currency = 'USD'
    }
}