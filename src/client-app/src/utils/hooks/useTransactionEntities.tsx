import {Account} from "../../models/accounts/account";
import {useStore} from "../../stores/store";
import {useEffect} from "react";

const useTransactionEntities = (account?: Account) => {
    const {transactionEntityStore} = useStore()

    useEffect(() => {
        const loadTransactionEntities = async () => await transactionEntityStore.loadTransactionEntities()

        if(account) {
            const requiredTransactionEntities = Array.from(new Set([
                ...account.transactions.flatMap(t => t.senderId),
                ...account.transactions.flatMap(t => t.recipientId)
            ]))

            if(transactionEntityStore.transactionEntities.length < requiredTransactionEntities.length) {
                loadTransactionEntities()
            }
        } else {
            loadTransactionEntities()
        }

    }, [account, transactionEntityStore])

    return transactionEntityStore.transactionEntities
}

export default useTransactionEntities