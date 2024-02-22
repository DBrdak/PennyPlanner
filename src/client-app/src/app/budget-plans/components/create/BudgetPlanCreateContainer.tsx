import {Box, Button, Divider, Grid} from "@mui/material";
import {observer} from "mobx-react-lite";
import useCategories from "../../../../utils/hooks/useCategories";
import BudgetedCategoryCreateCard from "./BudgetedCategoryCreateCard";
import theme from "../../../theme";
import {BudgetedCategoryCreateSubmitButton} from "./BudgetedCategoryCreateSubmitButton";

export default observer(function BudgetPlanCreateContainer() {
    const categories = useCategories()

    const getIncomeCategories = () => categories.filter(c => c.type.toLowerCase() === 'income')
    const getOutcomeCategories = () => categories.filter(c => c.type.toLowerCase() === 'outcome')

    return (
        <Grid container spacing={3} sx={{
            height: '100%',
            marginTop: theme.spacing(2),
            position: 'relative',
            overflow: 'auto'
        }}>
            {getIncomeCategories().map(category => (
                <BudgetedCategoryCreateCard key={category.transactionCategoryId} category={category} />
            ))}
            <BudgetedCategoryCreateCard key={'newIncomeCategory'} />
            <Grid item xs={12}>
                <Divider variant={'fullWidth'} />
            </Grid>
            {getOutcomeCategories().map(category => (
                <BudgetedCategoryCreateCard key={category.transactionCategoryId} category={category} />
            ))}
            <BudgetedCategoryCreateCard key={'newOutcomeCategory'} />
            <BudgetedCategoryCreateSubmitButton />
        </Grid>
    );
})