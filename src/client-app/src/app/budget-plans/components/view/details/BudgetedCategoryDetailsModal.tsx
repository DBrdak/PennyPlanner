import {BudgetedTransactionCategory} from "../../../../../models/budgetPlans/budgetedTransactionCategory";
import {Transaction} from "../../../../../models/transactions/transaction";
import {Button, ButtonGroup, Grid, InputAdornment, Typography} from "@mui/material";
import {TransactionCategory} from "../../../../../models/transactionCategories/transactionCategory";
import formatMoney from "../../../../../utils/formatters/moneyFormatter";
import {BudgetedCategoryTransactionsTable} from "./BudgetedCategoryTransactionsTable";
import React, {useState} from "react";
import {
    UpdateBudgetPlanCategoryValues
} from "../../../../../models/requests/budgetPlans/updateBudgetPlanCategoryValues";
import {Form, Formik} from "formik";
import * as yup from 'yup'
import {decimal_MAX} from "../../../../../utils/constants/numeric";
import MyTextInput from "../../../../../components/MyTextInput";
import {useNavigate} from "react-router-dom";

interface BudgetedCategoryDetailsModalProps {
    budgetedCategory: BudgetedTransactionCategory,
    transactions: Transaction[],
    transactionCategory: TransactionCategory
    onEdit: (updateValues: UpdateBudgetPlanCategoryValues) => void
    onDelete: () => void
}

export function BudgetedCategoryDetailsModal({ budgetedCategory, transactions, transactionCategory, onEdit, onDelete }: BudgetedCategoryDetailsModalProps) {
    const [editMode, setEditMode] = useState(false)
    const navigate = useNavigate()

    const validationSchema = yup.object({
        newBudgetAmount: yup.number()
            .required()
            .min(0)
            .max(decimal_MAX)
            .notOneOf([budgetedCategory.budgetedAmount.amount])
    })

    return (
        <Formik initialValues={new UpdateBudgetPlanCategoryValues(budgetedCategory.budgetedAmount.amount)}
                onSubmit={onEdit} validationSchema={validationSchema}>
            {({setValues, values, isValid, handleSubmit}) => (
                    <Form autoComplete='off'>
                        <Grid container spacing={2} sx={{
                            width: '600px',
                            maxWidth: '95vw'
                        }}>
                            <Grid item xs={6}>
                                <Typography variant={'h6'}>
                                    Category
                                </Typography>
                            </Grid>
                            <Grid item xs={6}>
                                <Typography variant={'h6'} textAlign={'right'}>
                                    {transactionCategory.value}
                                </Typography>
                            </Grid>
                            <Grid item xs={6}>
                                <Typography variant={'h6'}>
                                    Actual amount
                                </Typography>
                            </Grid>
                            <Grid item xs={6}>
                                <Typography variant={'h6'} textAlign={'right'}>
                                    {formatMoney(budgetedCategory.actualAmount)}
                                </Typography>
                            </Grid>
                            <Grid item xs={6}>
                                <Typography variant={'h6'}>
                                    Budgeted amount
                                </Typography>
                            </Grid>
                            <Grid item xs={6} sx={{
                                display: 'flex', justifyContent: 'right'
                            }}>
                                {
                                    editMode ?
                                        <MyTextInput
                                            style={{width: '60%', maxWidth: '500px'}}
                                            placeholder={'Budgeted Amount'}
                                            name={'newBudgetAmount'}
                                            type={'number'}
                                            inputProps={{endAdornment: <InputAdornment position="end">{budgetedCategory.budgetedAmount.currency}</InputAdornment>}}
                                        />
                                        :
                                        <Typography variant={'h6'} textAlign={'right'}>
                                            {formatMoney(budgetedCategory.budgetedAmount)}
                                        </Typography>
                                }
                            </Grid>
                            <Grid item xs={12}>
                                {
                                    transactions.length > 0 &&
                                        <BudgetedCategoryTransactionsTable
                                            transactions={transactions}
                                        />
                                }
                            </Grid>
                            <Grid item xs={12} textAlign={'center'} marginTop={2}>
                                <ButtonGroup fullWidth>
                                    {
                                        editMode ?
                                            <Button onClick={() => setEditMode(false)}
                                                    variant={'contained'} color={'inherit'} sx={{width: '50%'}}>
                                                Exit
                                            </Button>
                                            :
                                            <Button onClick={onDelete} variant={'contained'} color={'error'} sx={{width: '50%'}}>
                                                Un-Budget Category
                                            </Button>
                                    }
                                    <Button onClick={() => editMode ? handleSubmit() : setEditMode(true)}
                                            variant={'outlined'} color={'secondary'} sx={{width: '50%'}}>
                                        Re-Budget Category
                                    </Button>
                                </ButtonGroup>
                            </Grid>
                        </Grid>
                    </Form>
                )}
        </Formik>
    );
}