import {Box, Button, CircularProgress, Divider, Grid} from "@mui/material";
import {observer} from "mobx-react-lite";
import useCategories from "../../../../utils/hooks/useCategories";
import BudgetedCategoryCreateCard from "./BudgetedCategoryCreateCard";
import theme from "../../../theme";
import {BudgetedCategoryCreateSubmitButton} from "./BudgetedCategoryCreateSubmitButton";
import {useStore} from "../../../../stores/store";
import {useNewCategoryValues} from "../../../../utils/hooks/useNewCategoryValues";

export default observer(function BudgetPlanCreateContainer() {
    const categories = useCategories()
    const {categoryStore, budgetPlanStore} = useStore()
    const {newIncomeCategoryValues} = useNewCategoryValues('income')
    const {newOutcomeCategoryValues} = useNewCategoryValues('outcome')

    const getIncomeCategories = () =>
        categories.filter(c => c.type.toLowerCase() === 'income')
    const getOutcomeCategories = () =>
        categories.filter(c => c.type.toLowerCase() === 'outcome')

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
                        <BudgetedCategoryCreateCard key={category.transactionCategoryId} category={category} />
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
                        <BudgetedCategoryCreateCard key={category.transactionCategoryId} category={category} />
                    ))
                }

                {
                    newOutcomeCategoryValues.map((_, index) => (
                        <BudgetedCategoryCreateCard key={`newOutcomeCategory${index}`} newCategoryType={'outcome'} />
                    ))
                }

                <BudgetedCategoryCreateSubmitButton
                    onSubmit={() => budgetPlanStore.setNewBudgetPlan()}
                    disabled={!budgetPlanStore.isNewBudgetPlanCreatePossible()}
                />
            </Grid>
    )
})