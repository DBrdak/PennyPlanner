import {TransactionCategory} from "../../../../models/transactionCategories/transactionCategory";
import {Grid, IconButton, Paper, Typography} from "@mui/material";
import theme from "../../../theme";
import {BudgetedCategoryCreateForm} from "./BudgetedCategoryCreateForm";
import {observer} from "mobx-react-lite";
import {Add} from "@mui/icons-material";
import {useEffect, useState} from "react";
import {
    AddTransactionCategoryForm
} from "../../../settings/transactionCategories/components/AddTransactionCategoryForm";
import {AddTransactionCategoryCommand} from "../../../../models/requests/categories/addTransactionCategoryCommand";
import {useStore} from "../../../../stores/store";
import {calcColorForCategory} from "../../../../utils/calculators/layoutCalculator";
import {
    BudgetedTransactionCategoryValues
} from "../../../../models/requests/budgetPlans/budgetedTransactionCategoryValues";

interface BudgetedCategoryCreateCardProps {
    category?: TransactionCategory
    newCategoryType?: 'income' | 'outcome'
}

export default observer(function BudgetedCategoryCreateCard({category, newCategoryType}: BudgetedCategoryCreateCardProps) {
    const { budgetPlanStore, categoryStore} = useStore()
    const [newCategoryValue, setNewCategoryValue] = useState<string>()
    const [existingCategoryValues, setExistingCategoryValues] = useState<string[]>([])

    useEffect(() => {
        setExistingCategoryValues([
            ...categoryStore.categories.flatMap(c => c.value),
            ...budgetPlanStore.newBudgetedCategories.flatMap(bc => bc.categoryValue)
            ])
    }, [budgetPlanStore.newBudgetedCategories, categoryStore.categories])

    const handleSubmit = (values: BudgetedTransactionCategoryValues) => {
        budgetPlanStore.setBudgetedCategory(values)
        setNewCategoryValue(values.categoryValue)
    }

    const handleCancel = () => {
        category && budgetPlanStore.removeBudgetedCategory(category.value)
        newCategoryValue && budgetPlanStore.removeBudgetedCategory(newCategoryValue)
    }

    return (
        <Grid item xs={12} md={4} lg={3} sx={{
            height: '40%',
            minHeight: '500px',
        }}>
            <Paper sx={{
                height: '100%',
                minHeight: '400px',
                borderRadius: '20px',
                padding: theme.spacing(3),
                position: 'relative'
            }}>
                {
                    category ?
                        <>

                            <BudgetedCategoryCreateForm
                                category={category}
                                onSubmit={handleSubmit}
                                onCancel={() => handleCancel()}
                            />
                        </>
                        :
                        newCategoryType &&
                            <BudgetedCategoryCreateForm
                                newCategoryType={newCategoryType}
                                onSubmit={handleSubmit}
                                onCancel={() => handleCancel()}
                                forbiddenCategoryValues={existingCategoryValues}
                            />
                }
            </Paper>
        </Grid>
    );
})