import {CircularProgress, Divider, Grid} from "@mui/material";
import {observer} from "mobx-react-lite";
import useCategories from "../../../../utils/hooks/useCategories";
import BudgetedCategoryCreateCard from "./BudgetedCategoryCreateCard";
import {BudgetedCategoryCreateSubmitButton} from "./BudgetedCategoryCreateSubmitButton";
import {useStore} from "../../../../stores/store";
import {useNewCategoryValues} from "../../../../utils/hooks/useNewCategoryValues";

export default observer(function BudgetPlanCreateContainer() {
    const categories = useCategories()
    const {categoryStore, budgetPlanStore} = useStore()
    const {newIncomeCategoryValues} = useNewCategoryValues('income', categories)
    const {newOutcomeCategoryValues} = useNewCategoryValues('outcome', categories)

    const getCategory = (id: string) =>
        categories.find(c => c.transactionCategoryId === id)

    const getIncomeCategories = () =>
        categories.filter(c => c.type.toLowerCase() === 'income')
    const getOutcomeCategories = () =>
        categories.filter(c => c.type.toLowerCase() === 'outcome')

    const handleSubmit = () => {
        budgetPlanStore.setNewBudgetPlan()
        budgetPlanStore.setEditMode(false)
    }

    return (
        categoryStore.loading || budgetPlanStore.loading ?
            <CircularProgress />
            :
            <Grid container spacing={2} sx={{
                height: '100%',
                position: 'relative',
            }}>
                {
                    getIncomeCategories().map(category => (
                        <BudgetedCategoryCreateCard
                            key={category.transactionCategoryId}
                            category={category}
                        />
                    ))
                }
                {
                    newIncomeCategoryValues.map((_, index) => (
                        <BudgetedCategoryCreateCard key={`newIncomeCategory${index}`} newCategoryType={'income'} />
                    ))
                }

                <Grid item xs={12}>
                    <Divider variant={'fullWidth'} />
                </Grid>

                {
                    getOutcomeCategories().map(category => (
                        <BudgetedCategoryCreateCard
                            key={category.transactionCategoryId}
                            category={category}
                        />
                    ))
                }

                {
                    newOutcomeCategoryValues.map((_, index) => (
                        <BudgetedCategoryCreateCard key={`newOutcomeCategory${index}`} newCategoryType={'outcome'} />
                    ))
                }

                <BudgetedCategoryCreateSubmitButton
                    onSubmit={() => handleSubmit()}
                    disabled={!budgetPlanStore.isNewBudgetPlanCreatePossible()}
                />
            </Grid>
    )
})