import {Account} from "../../models/accounts/account";
import {useStore} from "../../stores/store";
import {useEffect} from "react";

const useCategories = (account?: Account) => {
    const {categoryStore} = useStore()

    useEffect(() => {
        const loadCategories = async () => await categoryStore.loadCategories()

        if(account) {
            const requiredCategories = Array.from(new Set([
                ...account.transactions.map(t => t.category)
            ]))

            if(categoryStore.categories.length !== requiredCategories.length) {
                loadCategories()
            }
        } else {
            loadCategories()
        }

    }, [account, categoryStore])

    return categoryStore.categories
}

export default useCategories