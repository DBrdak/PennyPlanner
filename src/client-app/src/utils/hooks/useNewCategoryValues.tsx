import useCategories from "./useCategories";
import {useStore} from "../../stores/store";
import {useEffect, useState} from "react";

export function useNewCategoryValues(newCategoryType: 'income' | 'outcome') {
    const categories = useCategories()
    const {budgetPlanStore} = useStore()
    const [newOutcomeCategoryIndices, setNewOutcomeCategoryIndices] = useState<number[]>([0])
    const [newIncomeCategoryIndices, setNewIncomeCategoryIndices] = useState<number[]>([0])

    const getIncomeCategories = () =>
        categories.filter(c => c.type.toLowerCase() === 'income')
    const getOutcomeCategories = () =>
        categories.filter(c => c.type.toLowerCase() === 'outcome')

    useEffect(() => {
        const newCategories = newCategoryType === 'income'
            ? budgetPlanStore.newBudgetedCategories
                .filter(bc =>
                    getIncomeCategories()
                        .every(c =>
                            c.type.toLowerCase() === bc.categoryType.toLowerCase()
                            && c.value !== bc.categoryValue)
                ).map(bc => bc.categoryValue)
            : budgetPlanStore.newBudgetedCategories
                .filter(bc =>
                    getOutcomeCategories()
                        .every(c =>
                            c.type.toLowerCase() === bc.categoryType.toLowerCase()
                            && c.value !== bc.categoryValue)
                ).map(bc => bc.categoryValue)

        if(newCategoryType === 'outcome') {
            const outcomeDiff = newCategories.length + 1 - newOutcomeCategoryIndices.length
            if (outcomeDiff > 0) {
                setNewOutcomeCategoryIndices(prev => [
                    ...prev,
                    ...Array.from({ length: outcomeDiff }, (_, index) => 1 + index)
                ]);
            } else if (outcomeDiff < 0) {
                setNewOutcomeCategoryIndices(prev => prev.slice(0, outcomeDiff));
            }
        } else {
            const incomeDiff = newCategories.length + 1 - newIncomeCategoryIndices.length


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