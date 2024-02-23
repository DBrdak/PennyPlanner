import {Account} from "../../models/accounts/account";
import {useStore} from "../../stores/store";
import {useEffect} from "react";

const useSelectedCategory = () => {
    const {categoryStore} = useStore()

    useEffect(() => {
        const loadCategories = async () => await categoryStore.loadCategories()

        if(categoryStore.categories.length < 1) {
            loadCategories()
        }

    }, [categoryStore])

    return categoryStore.selectedCategory
}

export default useSelectedCategory