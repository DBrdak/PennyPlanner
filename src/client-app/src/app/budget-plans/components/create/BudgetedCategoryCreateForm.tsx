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
import {calcColorForCategory} from "../../../../utils/calculators/layoutCalculator";
import ValidationConstants from "../../../../utils/constants/validationConstants";

interface BudgetedCategoryCreateFormProps {
    category?: TransactionCategory
    newCategoryType?: 'income' | 'outcome'
    forbiddenCategoryValues?: string[]
    onSubmit: (values: BudgetedTransactionCategoryValues) => void
    onCancel: () => void
}

export function BudgetedCategoryCreateForm({category, newCategoryType, forbiddenCategoryValues, onSubmit, onCancel}: BudgetedCategoryCreateFormProps) {
    const [isCategoryBudgeted, setIsCategoryBudgeted] = useState(false)
    const [isFormVisible, setIsFormVisible] = useState(false)
    const initialValues = category ?
        new BudgetedTransactionCategoryValues(category) :
        new BudgetedTransactionCategoryValues(undefined, newCategoryType)

    const validationSchema = yup.object({
        budgetedAmount: yup.number()
            .required("Budgeted amount is required")
            .min(0.1, "Budgeted amount must be positive")
            .max(decimal_MAX, "Budgeted amount is to high"),
        categoryValue: yup.string()
            .required('Category is required')
            .max(30, 'Transaction category name must be between 1 and 30 characters')
            .matches(ValidationConstants.noSpecialCharactersPattern, 'Special characters are not allowed in Transaction category name')
            .notOneOf(forbiddenCategoryValues || [], "Category value must be unique")
    })

    const handleCategoryBudget = (values: BudgetedTransactionCategoryValues) => {
        onSubmit(values)
        setIsCategoryBudgeted(true)
    }

    const handleCategoryUnBudget = () => {
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
                            category &&
                                <Grid item xs={12} sx={{height: '10%'}}>
                                    <Typography variant={'h4'} textAlign={'center'}>
                                        {category.value}
                                    </Typography>
                                    <Typography variant={'subtitle1'} textAlign={'center'} sx={{color: calcColorForCategory(category.type)}}>
                                        {category.type}
                                    </Typography>
                                </Grid>
                        }
                        {
                            newCategoryType && isFormVisible &&
                                <Grid item xs={12} >
                                    <MyTextInput
                                        type={'text'}
                                        name={'categoryValue'}
                                        placeholder={'New Category Value'}
                                        showErrors
                                        style={{display: 'flex', alignItems: 'center'}}
                                        inputProps={{
                                            endAdornment:
                                                <IconButton onClick={() => {
                                                    resetForm()
                                                    handleCategoryUnBudget()
                                                }}>
                                                    <Clear/>
                                                </IconButton>
                                        }}
                                    />
                                    <Typography variant={'subtitle1'} textAlign={'center'} sx={{
                                        color: calcColorForCategory(newCategoryType),
                                    }}>
                                        {newCategoryType[0].toUpperCase() + newCategoryType.slice(1)}
                                    </Typography>
                                </Grid>
                        }
                        <Grid item xs={12} sx={{
                            height: '100%'
                        }}>
                            {
                                isFormVisible ?
                                    <MyTextInput
                                        type={'number'}
                                        name={'budgetedAmount'}
                                        placeholder={'Budgeted amount'}
                                        showErrors
                                        style={{display: 'flex', alignItems: 'center'}}
                                        inputProps={{endAdornment:
                                                <IconButton onClick={() => {
                                                    resetForm()
                                                    handleCategoryUnBudget()
                                                }}>
                                                    <Clear />
                                                </IconButton>}}
                                    />
                                    :
                                    <Typography sx={{
                                        display: newCategoryType && 'flex',
                                        alignItems: 'center',
                                        justifyContent: 'center',
                                        height: '100%'
                                    }}>
                                        {category && 'Category not budgeted'}
                                        {newCategoryType && `New ${newCategoryType} category`}
                                    </Typography>
                            }
                        </Grid>
                        <Button variant={isCategoryBudgeted && isFormVisible ? 'outlined' : 'contained'}
                                color={category ? 'primary' : 'secondary'}
                                disabled={!isValid} onClick={() => {
                                    setIsFormVisible(true)
                                    handleSubmit()
                        }} sx={{
                            position: 'absolute',
                            bottom: 0, left: 0, right: 0,
                            borderRadius: '0px 0px 20px 20px',
                            height: '10%'
                        }}>
                            {
                                isCategoryBudgeted && isFormVisible ?
                                    'Re-budget'
                                    :
                                    category ?
                                        'Budget the category'
                                        :
                                        'Add and budget new category'
                            }
                        </Button>
                    </Form>
                )}
            </Formik>
        </Grid>
    );
}