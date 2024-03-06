export default class ValidationConstants {
    static noSpecialCharactersPattern = /^[^\s][\p{L}\d\s]*[^\s]$/u
    static passwordPattern = /^(?=.*[!@#$%^&*()-_=+{};:',.<>?])(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$/u
    static emailPattern = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/u
}