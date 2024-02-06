import {Transaction} from "../../models/transactions/transaction";

export const calculateBalanceForToday = (transactions: Transaction[]): number => {
    const today = new Date();
    const todayTransactions = transactions.filter(transaction =>
        isSameDay(new Date(transaction.transactionDateUtc), today)
    );

    return calculateBalance(todayTransactions);
};

export const calculateBalanceForCurrentMonth = (transactions: Transaction[]): number => {
    const currentMonth = new Date().getMonth();
    const currentMonthTransactions = transactions.filter(transaction =>
        new Date(transaction.transactionDateUtc).getMonth() === currentMonth
    );

    return calculateBalance(currentMonthTransactions);
};

const isSameDay = (date1: Date, date2: Date): boolean => {
    return (
        date1.getDate() === date2.getDate() &&
        date1.getMonth() === date2.getMonth() &&
        date1.getFullYear() === date2.getFullYear()
    );
};

const calculateBalance = (transactions: Transaction[]): number => {
    return transactions.reduce((balance, transaction) => {
        return Number((balance + transaction.transactionAmount.amount).toFixed(2));
    }, 0);
};