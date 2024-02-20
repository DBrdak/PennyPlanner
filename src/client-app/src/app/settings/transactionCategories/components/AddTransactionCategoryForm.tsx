import {Form, Formik} from "formik";
import MyTextInput from "../../../../components/MyTextInput";
import {Box, ButtonGroup, Grid, IconButton, Typography} from "@mui/material";
import {Clear, DeleteTwoTone, DoneTwoTone} from "@mui/icons-material";
import theme from "../../../theme";
import React from "react";
import * as yup from 'yup'
import {AddTransactionCategoryCommand} from "../../../../models/requests/categories/addTransactionCategoryCommand";
import ValidationConstants from "../../../../utils/constants/validationConstants";
import {
    AddTransactionSubcategoryCommand
} from "../../../../models/requests/subcategories/addTransactionSubcategoryCommand";

interface TransactionCategoryForm {
    type: "income" | "outcome"
    onSubmit: (command: AddTransactionCategoryCommand | AddTransactionSubcategoryCommand) => void
    onExit: () => void
    categoryId?: string
}

export function AddTransactionCategoryForm({type, onSubmit, onExit, categoryId}: TransactionCategoryForm) {

    const validationSchema = yup.object({
        value: yup.string()
            .required('Value is required')
            .max(30, 'Transaction category value must be between 1 and 30 characters')
            .matches(ValidationConstants.noSpecialCharactersPattern, 'Special characters are not allowed in transaction category value')
    })

    const capitalizedType = type.charAt(0).toUpperCase().concat(type.slice(1))


    return (
        <Formik
            initialValues={categoryId ?
                new AddTransactionSubcategoryCommand(categoryId)
                : new AddTransactionCategoryCommand(type)}
            onSubmit={onSubmit}
            validationSchema={validationSchema}>
            {({handleSubmit, setValues, resetForm, values, isValid}) => (
                <Form style={{height: '100%'}} className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                    <Grid container height={'100%'}>
                        <Grid item xs={12} sx={{
                            height: '70%',
                            display: 'flex',
                            justifyContent: 'center',
                            alignItems: 'center',
                            flexDirection: 'column'
                        }}>
                            <MyTextInput
                                name={'value'}
                                placeholder={'Value'}
                                type={'text'}
                                showErrors
                            />
                            <Typography variant={'subtitle1'} textAlign={'center'} color={
                                type === 'income' ? 'primary' : 'secondary'
                            }>
                                {!categoryId && capitalizedType}
                            </Typography>
                        </Grid>
                        <Grid item xs={12} sx={{
                            height: '30%',
                            display: 'flex',
                            justifyContent: 'center',
                            alignItems: 'center',
                        }}>
                            <ButtonGroup fullWidth>
                                <IconButton color={'inherit'} sx={{width: '50%', borderRadius: 0}} onClick={() => {
                                    resetForm()
                                    onExit()
                                }}>
                                    <Clear fontSize={'large'} />
                                </IconButton>
                                <IconButton disabled={!isValid} color={'success'} sx={{width: '50%', borderRadius: 0}} onClick={() => {
                                    handleSubmit()
                                    resetForm()
                                    onExit()
                                }}>
                                    <DoneTwoTone fontSize={'large'} />
                                </IconButton>
                            </ButtonGroup>
                        </Grid>
                    </Grid>
                </Form>
            )}
        </Formik>
    );
}