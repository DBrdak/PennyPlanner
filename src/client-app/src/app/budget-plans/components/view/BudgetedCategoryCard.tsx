import {BudgetedTransactionCategory} from "../../../../models/budgetPlans/budgetedTransactionCategory";
import {Divider, Grid, Paper, Typography} from "@mui/material";
import theme from "../../../theme";
import {TwoFactorPieChart} from "../../../../components/TwoFactorPieChart";
import {BudgetedCategoryCardActions} from "./BudgetedCategoryCardActions";
import {useState} from "react";
import {observer} from "mobx-react-lite";
import useCategories from "../../../../utils/hooks/useCategories";

interface BudgetedCategoryCardProps{
    budgetedCategory: BudgetedTransactionCategory
}

export default observer (function BudgetedCategoryCard({budgetedCategory}: BudgetedCategoryCardProps) {
    const categories = useCategories()
    const [editMode, setEditMode] = useState(false)

    const transactionCategory = categories.find(c => c.transactionCategoryId === budgetedCategory.categoryId)

    function handleTransactionsViewEnter() {

    }

    return (
        transactionCategory ?
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
                    <TwoFactorPieChart
                        actual={budgetedCategory.actualAmount.amount}
                        target={budgetedCategory.budgetedAmount.amount}
                        color={transactionCategory.type.toLowerCase() === 'income' ? 'positive' : 'negative'}
                    />

                    <Divider variant={"middle"} />

                    <Typography>
                        {transactionCategory?.value}
                    </Typography>

                    <BudgetedCategoryCardActions
                        onEditModeEnter={() => setEditMode(true)}
                        onTransactionsViewEnter={() => handleTransactionsViewEnter()}
                    />
                </Paper>
            </Grid>
            :
            null
    );
})