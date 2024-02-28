import {BudgetedTransactionCategory} from "../../../../models/budgetPlans/budgetedTransactionCategory";
import {Divider, Grid, Paper, Typography} from "@mui/material";
import theme from "../../../theme";
import {TwoFactorPieChart} from "../../../../components/TwoFactorPieChart";
import {BudgetedCategoryCardActions} from "./BudgetedCategoryCardActions";
import {useState} from "react";
import {observer} from "mobx-react-lite";
import useCategories from "../../../../utils/hooks/useCategories";
import {TransactionCategory} from "../../../../models/transactionCategories/transactionCategory";

interface BudgetedCategoryCardProps{
    categories: TransactionCategory[]
    budgetedCategory: BudgetedTransactionCategory
}

export default observer (function BudgetedCategoryCard({budgetedCategory, categories}: BudgetedCategoryCardProps) {
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
                    <Grid item xs={12} maxHeight={'70%'}>
                        <TwoFactorPieChart
                            actual={budgetedCategory.actualAmount.amount}
                            target={budgetedCategory.budgetedAmount.amount}
                            color={transactionCategory.type.toLowerCase() === 'income' ? 'positive' : 'negative'}
                            currency={budgetedCategory.budgetedAmount.currency}
                        />
                    </Grid>

                    <Grid item xs={12}>
                        <Divider variant={"middle"} />
                    </Grid>

                    <Grid item xs={12} marginTop={theme.spacing(1.5)}>
                        <Typography variant={'h4'}>
                            {transactionCategory?.value}
                        </Typography>
                    </Grid>

                    <Grid item xs={12} sx={{
                        position: 'absolute',
                        bottom: 0, left: 0, right: 0,
                    }}>
                        <BudgetedCategoryCardActions
                            onEditModeEnter={() => setEditMode(true)}
                            onTransactionsViewEnter={() => handleTransactionsViewEnter()}
                        />
                    </Grid>
                </Paper>
            </Grid>
            :
            null
    );
})