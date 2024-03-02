import {useStore} from "../../stores/store";
import {useEffect, useState} from "react";
import {TransactionCategory} from "../../models/transactionCategories/transactionCategory";

export function useNewCategoryValues(newCategoryType: 'income' | 'outcome', categories: TransactionCategory[]) {
    const {budgetPlanStore} = useStore()
    const [newOutcomeCategoryIndices, setNewOutcomeCategoryIndices] = useState<number[]>([0])
    const [newIncomeCategoryIndices, setNewIncomeCategoryIndices] = useState<number[]>([0])

    const getIncomeCategories = () =>
        categories.filter(c => c.type.toLowerCase() === 'income')
    const getOutcomeCategories = () =>
        categories.filter(c => c.type.toLowerCase() === 'outcome')

    useEffect(() => {
        const newOutcomeCategories = budgetPlanStore.newBudgetedCategories
            .filter(bc =>
                bc.categoryType.toLowerCase() === 'outcome' &&
                getOutcomeCategories()
                    .every(c =>
                        c.value.toLowerCase() !== bc.categoryValue.toLowerCase())
            ).map(bc => bc.categoryValue)

        const newIncomeCategories = budgetPlanStore.newBudgetedCategories
                .filter(bc =>
                    bc.categoryType.toLowerCase() === 'income' &&
                    getIncomeCategories()
                        .every(c =>
                            c.value.toLowerCase() !== bc.categoryValue.toLowerCase()))
                .map(bc => bc.categoryValue)

        if(newCategoryType === 'outcome' && newOutcomeCategories) {
            const outcomeDiff = newOutcomeCategories.length + 1 - newOutcomeCategoryIndices.length
            if (outcomeDiff > 0) {
                setNewOutcomeCategoryIndices(prev => [
                    ...prev,
                    ...Array.from({ length: outcomeDiff }, (_, index) => 1 + index)
                ]);
            } else if (outcomeDiff < 0) {
                setNewOutcomeCategoryIndices(prev => prev.slice(0, outcomeDiff));
            }
        } else if (newCategoryType === 'income' && newIncomeCategories) {
            const incomeDiff = newIncomeCategories.length + 1 - newIncomeCategoryIndices.length

            if (incomeDiff > 0) {
                setNewIncomeCategoryIndices(prev => [
                    ...prev,
                    ...Array.from({ length: incomeDiff }, (_, index) => 1 + index)
                ]);
            } else if (incomeDiff < 0) {
                setNewIncomeCategoryIndices(prev => prev.slice(0, incomeDiff));
            }
        }


    }, [budgetPlanStore.newBudgetedCategories, categories])

    return {
        newOutcomeCategoryValues: newOutcomeCategoryIndices,
        newIncomeCategoryValues: newIncomeCategoryIndices
    }
}