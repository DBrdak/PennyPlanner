import {Transaction} from "../models/transactions/transaction";
import formatDate from "./formatters/dateFormatter";
import {TransactionCategory} from "../models/transactionCategories/transactionCategory";

const groupBy = (transactions: Transaction[], criterion: string): Record<string, Transaction[]> => {
    const groupedTransactions: Record<string, Transaction[]> = {};

    transactions.forEach((transaction) => {
        let key

        switch (criterion) {
            case 'day':
                key = formatDate(transaction.transactionDateUtc).slice(0, 10)
                break
            case 'month':
                key = formatDate(transaction.transactionDateUtc).slice(3, 10)
                break
            case 'year':
                key = formatDate(transaction.transactionDateUtc).slice(6, 10)
                break
            case 'type':
                key = transaction.transactionAmount.amount >= 0 ? 'Income' : 'Outcome'
                break
            case 'entity':
                if(transaction['recipientId' as keyof Transaction]){
                    key = transaction['recipientId' as keyof Transaction] as string
                } else if(transaction['senderId' as keyof Transaction]){
                    key = transaction['senderId' as keyof Transaction] as string
                } else if(transaction['toAccountId' as keyof Transaction]){
                    key = transaction['toAccountId' as keyof Transaction] as string
                } else if(transaction['fromAccountId' as keyof Transaction]){
                    key = transaction['fromAccountId' as keyof Transaction] as string
                } else {
                    key = 'Adjustments'
                }
                break
            case 'category':
                key = (transaction['category' as keyof Transaction] as TransactionCategory)?.transactionCategoryId || 'Adjustments'
                break
        }

        if (key && !groupedTransactions[key]) {
            groupedTransactions[key] = [];
        }

        key && groupedTransactions[key].push(transaction);
    });

    return groupedTransactions;
};

export default groupBy