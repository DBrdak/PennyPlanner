import {TransactionCategory} from "../../../../models/transactionCategories/transactionCategory";
import {Form, Formik} from "formik";
import {Box, Button, ButtonGroup, Grid, IconButton, InputAdornment, Stack, Typography} from "@mui/material";
import {Clear, DeleteTwoTone, Undo} from "@mui/icons-material";
import theme from "../../../theme";
import MyTextInput from "../../../../components/MyTextInput";
import GroupDropdown from "../../../../components/transactionsTable/GroupDropdown";
import React, {useState} from "react";
import * as yup from 'yup'
import {
    BudgetedTransactionCategoryValues
} from "../../../../models/requests/budgetPlans/budgetedTransactionCategoryValues";
import {decimal_MAX} from "../../../../utils/constants/numeric";

interface BudgetedCategoryCreateFormProps {
    category: TransactionCategory
    onSubmit: (values: BudgetedTransactionCategoryValues) => void
    onCancel: () => void
}

export function BudgetedCategoryCreateForm({category, onSubmit, onCancel}: BudgetedCategoryCreateFormProps) {
    const [isCategoryBudgeted, setIsCategoryBudgeted] = useState(false)
    const [isFormVisible, setIsFormVisible] = useState(false)
    const initialValues = new BudgetedTransactionCategoryValues(category)

    const validationSchema = yup.object({
        budgetedAmount: yup.number()
            .required("Budgeted amount is required")
            .min(0.1, "Budgeted amount must be positive")
            .max(decimal_MAX, "Budgeted amount is to high")
    })

    const handleCategoryBudget = (values: BudgetedTransactionCategoryValues) => {
        onSubmit(values)
        setIsCategoryBudgeted(true)
    }

    const handleCategoryUnbudget = () => {
        setIsFormVisible(false)
        setIsCategoryBudgeted(false)
        onCancel()
    }

    return (
        <Grid item xs={12} sx={{height: '80%'}}>
            <Formik sx={{width: '100%', height: '100%'}} initialValues={initialValues} onSubmit={handleCategoryBudget} validationSchema={validationSchema}>
                {({setValues, values, isValid, handleSubmit, resetForm}) => (
                    <Form autoComplete='off'
                          style={{
                              width: '100%',
                              display: 'flex',
                              justifyContent: 'center',
                              alignItems: 'center',
                              flexDirection: 'column',
                              height: '100%',
                          }}>
                        {
                            isFormVisible ?
                                <MyTextInput
                                    type={'number'}
                                    name={'budgetedAmount'}
                                    placeholder={'Budgeted amount'}
                                    showErrors
                                    style={{width: '350px', maxWidth: '70%'}}
                                    inputProps={{endAdornment:
                                        <IconButton onClick={() => {
                                            resetForm()
                                            handleCategoryUnbudget()
                                        }}>
                                            <Clear />
                                        </IconButton>}}
                                />
                                :
                                <Typography>
                                    Category not budgeted
                                </Typography>
                        }
                        <Button variant={isCategoryBudgeted && isFormVisible ? 'outlined' : 'contained'}
                                disabled={!isValid} onClick={() => {
                                    setIsFormVisible(true)
                                    handleSubmit()
                        }} sx={{
                            position: 'absolute',
                            bottom: 0, left: 0, right: 0,
                            borderRadius: '0px 0px 20px 20px',
                            height: '10%'
                        }}>
                            {isCategoryBudgeted && isFormVisible ? 'Re-budget' :  'Budget the category'}
                        </Button>
                    </Form>
                )}
            </Formik>
        </Grid>
    );
}