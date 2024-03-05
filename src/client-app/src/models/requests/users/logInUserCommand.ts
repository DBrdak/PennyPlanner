export interface LogInUserCommand {
    email: string
    password: string
}

export class LogInUserCommand implements LogInUserCommand {
    constructor() {
        this.email = ''
        this.password = ''
    }
}