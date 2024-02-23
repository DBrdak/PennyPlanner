import {BudgetedTransactionCategory} from "../../../../models/budgetPlans/budgetedTransactionCategory";
import {Grid} from "@mui/material";
import {BudgetedCategoryCard} from "./BudgetedCategoryCard";

interface BudgetedCategoriesContainerProps {
    budgetedCategories: BudgetedTransactionCategory[]
}

export function BudgetedCategoriesContainer({budgetedCategories}: BudgetedCategoriesContainerProps) {
    return (
        <Grid container sx={{width: '100%', height: '100%'}}>
            {budgetedCategories.map((budgetedCategory, index) => (
                <BudgetedCategoryCard key={index} budgetedCategory={budgetedCategory} />
            ))}
        </Grid>
    );
}