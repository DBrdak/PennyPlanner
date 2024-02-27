import {BudgetedTransactionCategory} from "../../../../models/budgetPlans/budgetedTransactionCategory";
import {Divider, Grid} from "@mui/material";
import BudgetedCategoryCard from "./BudgetedCategoryCard";

interface BudgetedCategoriesContainerProps {
    budgetedCategories: BudgetedTransactionCategory[]
}

export function BudgetedCategoriesContainer({budgetedCategories}: BudgetedCategoriesContainerProps) {
    return (
        <Grid container spacing={2} sx={{
            height: '100%',
            position: 'relative',
        }}>
            {
                budgetedCategories.map((budgetedCategory, index) => (
                    <BudgetedCategoryCard key={index} budgetedCategory={budgetedCategory} />
                ))
            }

            <Grid item xs={12}>
                <Divider variant={'fullWidth'} />
            </Grid>

            {
                budgetedCategories.map((budgetedCategory, index) => (
                    <BudgetedCategoryCard key={index} budgetedCategory={budgetedCategory} />
                ))
            }

        </Grid>
    );
}