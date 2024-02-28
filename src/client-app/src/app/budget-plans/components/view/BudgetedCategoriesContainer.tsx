import {BudgetedTransactionCategory} from "../../../../models/budgetPlans/budgetedTransactionCategory";
import {Divider, Grid} from "@mui/material";
import BudgetedCategoryCard from "./BudgetedCategoryCard";
import {BudgetPlanEditButton} from "./BudgetPlanEditButton";
import {useState} from "react";
import {observer} from "mobx-react-lite";
import theme from "../../../theme";
import useCategories from "../../../../utils/hooks/useCategories";

interface BudgetedCategoriesContainerProps {
    budgetedCategories: BudgetedTransactionCategory[]
}

export default observer (function BudgetedCategoriesContainer({budgetedCategories}: BudgetedCategoriesContainerProps) {
    const categories = useCategories()

    const getCategoryById = (id: string) => categories.find(c => c.transactionCategoryId === id)

    const getBudgetedCategoriesByType = (type: 'income' | 'outcome') => budgetedCategories.filter(btc =>
        getCategoryById(btc.categoryId)?.type.toLowerCase() === type)

    return (
        <Grid container spacing={2} sx={{
            height: '100%',
            position: 'relative',
            paddingBottom: theme.spacing(15)
        }}>
            {
                getBudgetedCategoriesByType('income').map((budgetedCategory, index) => (
                    <BudgetedCategoryCard key={index} budgetedCategory={budgetedCategory} categories={categories} />
                ))
            }

            {
                getBudgetedCategoriesByType('income').length > 0 && getBudgetedCategoriesByType('outcome').length > 0 &&
                    <Grid item xs={12}>
                        <Divider variant={'fullWidth'} />
                    </Grid>
            }

            {
                getBudgetedCategoriesByType('outcome').map((budgetedCategory, index) => (
                    <BudgetedCategoryCard key={index} budgetedCategory={budgetedCategory} categories={categories} />
                ))
            }
        </Grid>
    );
})