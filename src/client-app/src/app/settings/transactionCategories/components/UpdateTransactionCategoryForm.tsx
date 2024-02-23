import {TransactionCategory} from "../../../../models/transactionCategories/transactionCategory";
import {Form, Formik} from "formik";
import React from "react";
import * as yup from 'yup'
import ValidationConstants from "../../../../utils/constants/validationConstants";
import {Box, ButtonGroup, Grid, IconButton, Typography} from "@mui/material";
import theme from "../../../theme";
import {Clear, DeleteTwoTone, DoneTwoTone, EditTwoTone, KeyboardArrowDown} from "@mui/icons-material";
import MyTextInput from "../../../../components/MyTextInput";
import {TransactionSubcategory} from "../../../../models/transactionSubcategories/transactionSubcategory";

interface UpdateTransactionCategoryFormProps {
    onDelete: (transactionCategoryId: string) => void
    onExit: () => void
    onSubmit: (transactionCategoryId: string, newValue: string) => void
    transactionCategory: TransactionCategory | TransactionSubcategory
}

export function UpdateTransactionCategoryForm({transactionCategory, onExit, onDelete, onSubmit}: UpdateTransactionCategoryFormProps) {

    const validationSchema = yup.object({
        newValue: yup.string()
            .required('Value is required')
            .max(30, 'Transaction category name must be between 1 and 30 characters')
            .matches(ValidationConstants.noSpecialCharactersPattern, 'Special characters are not allowed in transaction category name')
            .notOneOf([transactionCategory.value], "New value must differ from current value")
    })

    return (
        <Formik
            initialValues={{newValue: transactionCategory.value}}
            onSubmit={(values) =>
                onSubmit((transactionCategory as TransactionCategory).transactionCategoryId
                    || (transactionCategory as TransactionSubcategory).transactionSubcategoryId,
                    values.newValue)}
            validationSchema={validationSchema}
            validateOnMount
        >
            {({handleSubmit, setValues, values, isValid, handleChange}) => (
                <Form style={{height: '100%', width: '100%'}} className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                    <Grid container height={'100%'}>
                        <Grid item xs={12} sx={{
                            height: '70%',
                            display: 'flex',
                            justifyContent: 'center',
                            alignItems: 'center',
                            flexDirection: 'column'
                        }}>
                            <MyTextInput
                                name={'newValue'}
                                placeholder={'New Value'}
                                type={'text'}
                                showErrors
                                inputProps={{endAdornment:
                                        <IconButton
                                            disabled={!isValid}
                                            sx={{borderRadius: 0}}
                                            color={'success'}
                                            onClick={() => {
                                                handleSubmit()
                                                onExit()
                                            }}
                                        >
                                            <DoneTwoTone fontSize={'large'} />
                                        </IconButton>
                                }}
                            />
                            {
                                (transactionCategory as TransactionCategory).type &&
                                    <Typography variant={'subtitle1'} textAlign={'center'} color={
                                        (transactionCategory as TransactionCategory).type.toLowerCase() === 'income' ? 'primary' : 'secondary'
                                    }>
                                        {(transactionCategory as TransactionCategory).type}
                                    </Typography>
                            }
                        </Grid>
                        <Grid item xs={12} sx={{
                            height: '30%',
                            display: 'flex',
                            justifyContent: 'center',
                            alignItems: 'center',
                            flexDirection: 'column'
                        }}>
                            <ButtonGroup fullWidth>
                                <IconButton color={'error'} sx={{width: '50%', borderRadius: 0}} onClick={() => onDelete(
                                    (transactionCategory as TransactionCategory).transactionCategoryId ||
                                    (transactionCategory as TransactionSubcategory).transactionSubcategoryId
                                )}>
                                    <DeleteTwoTone fontSize={'large'} />
                                </IconButton>
                                <IconButton color={'inherit'} sx={{width: '50%', borderRadius: 0}} onClick={() => onExit()}>
                                    <Clear fontSize={'large'} />
                                </IconButton>
                            </ButtonGroup>
                                <IconButton sx={{
                                    width: '100%',
                                    borderRadius: 0,
                                    flexDirection: 'column'
                                }}>
                                    <Typography variant={'caption'}>
                                        Subcategories
                                    </Typography>
                                    <KeyboardArrowDown />
                                </IconButton>
                        </Grid>
                    </Grid>
                </Form>
            )}
        </Formik>
    );
}