import {BudgetedTransactionCategory} from "../../../../models/budgetPlans/budgetedTransactionCategory";
import {CircularProgress, Divider, Grid, Paper, Typography} from "@mui/material";
import theme from "../../../theme";
import {TwoFactorPieChart} from "../../../../components/TwoFactorPieChart";
import {BudgetedCategoryCardActions} from "./BudgetedCategoryCardActions";
import {useEffect, useState} from "react";
import {observer} from "mobx-react-lite";
import useCategories from "../../../../utils/hooks/useCategories";
import {TransactionCategory} from "../../../../models/transactionCategories/transactionCategory";
import {useStore} from "../../../../stores/store";
import {BudgetedCategoryDetailsModal} from "./details/BudgetedCategoryDetailsModal";
import {BudgetPlan} from "../../../../models/budgetPlans/budgetPlan";
import {UpdateBudgetPlanCategoryValues} from "../../../../models/requests/budgetPlans/updateBudgetPlanCategoryValues";

interface BudgetedCategoryCardProps{
    categories: TransactionCategory[]
    budgetedCategory: BudgetedTransactionCategory
}

export default observer (function BudgetedCategoryCard({budgetedCategory, categories}: BudgetedCategoryCardProps) {
    const {modalStore, budgetPlanStore} = useStore()
    const [loading, setLoading] = useState(false)
    const transactionCategory = categories.find(c => c.transactionCategoryId === budgetedCategory.categoryId)
    const transactions = budgetPlanStore.budgetPlan?.transactions
        .filter(t => t.category?.transactionCategoryId === budgetedCategory.categoryId) || []

    const handleDelete = () => {
        if (transactionCategory){
            setLoading(true)
            budgetPlanStore.updateBudgetPlan(transactionCategory.transactionCategoryId, new UpdateBudgetPlanCategoryValues())
                .then(() => setLoading(false))
            modalStore.closeModal()
        }
    }

    const handleEdit = (updateValues: UpdateBudgetPlanCategoryValues) => {
        if (transactionCategory){
            setLoading(true)
            budgetPlanStore.updateBudgetPlan(transactionCategory.transactionCategoryId, updateValues)
                .then(() => setLoading(false))
            modalStore.closeModal()
        }
    }

    function handleDetailedViewEnter() {
        transactionCategory &&
            modalStore.openModal(
                <BudgetedCategoryDetailsModal
                    budgetedCategory={budgetedCategory}
                    transactionCategory={transactionCategory}
                    transactions={transactions}
                    onDelete={handleDelete}
                    onEdit={handleEdit}
                />
            )
    }

    return (
        transactionCategory ?
            <Grid item xs={12} md={4} lg={3} sx={{
                height: '500px',
                zIndex: 100
            }}>
                <Paper sx={{
                    height: '100%',
                    minHeight: '400px',
                    borderRadius: '20px',
                    padding: theme.spacing(3),
                    position: 'relative',
                    width: '100%',
                }}>
                    {
                        loading ?
                            <Grid item xs={12} sx={{
                                width: '100%', height: '100%',
                                display:'flex', alignItems: 'center', justifyContent: 'center',
                            }}>
                                <CircularProgress/>
                            </Grid> :
                            <>
                                <Grid item xs={12} maxHeight={'70%'}>
                                    <TwoFactorPieChart
                                        actual={budgetedCategory.actualAmount.amount}
                                        target={budgetedCategory.budgetedAmount.amount}
                                        color={transactionCategory.type.toLowerCase() === 'income' ? 'positive' : 'negative'}
                                        currency={budgetedCategory.budgetedAmount.currency}
                                    />
                                </Grid>

                                <Grid item xs={12}>
                                    <Divider variant={"middle"}/>
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
                                        onDetailedViewEnter={() => handleDetailedViewEnter()}
                                    />
                                </Grid>
                            </>
                    }
                </Paper>
            </Grid>
            :
            null
    );
})