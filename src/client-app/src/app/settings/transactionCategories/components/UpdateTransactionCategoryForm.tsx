import {TransactionCategory} from "../../../../models/transactionCategories/transactionCategory";
import {Form, Formik} from "formik";
import React from "react";
import * as yup from 'yup'
import ValidationConstants from "../../../../utils/constants/validationConstants";
import {Box, IconButton, Typography} from "@mui/material";
import theme from "../../../theme";
import {Clear, DeleteTwoTone, DoneTwoTone} from "@mui/icons-material";
import MyTextInput from "../../../../components/MyTextInput";

interface UpdateTransactionCategoryFormProps {
    onDelete: (transactionCategoryId: string) => void
    onExit: () => void
    onSubmit: (transactionCategoryId: string, newValue: string) => void
    transactionCategory: TransactionCategory
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
            onSubmit={(values) => onSubmit(transactionCategory.transactionCategoryId, values.newValue)}
            validationSchema={validationSchema}
            validateOnMount
        >
            {({handleSubmit, setValues, values, isValid, handleChange}) => (
                <Form style={{}} className='ui form' onSubmit={handleSubmit} autoComplete='off'>
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
                    <Typography variant={'subtitle1'} textAlign={'center'} color={
                        transactionCategory.type.toLowerCase() === 'income' ? 'primary' : 'secondary'
                    }>
                        {transactionCategory.type}
                    </Typography>
                    <Box sx={{
                        position: 'absolute',
                        padding: theme.spacing(2),
                        bottom: 0, left: 0, right: 0,
                        height: '20%',
                        display: 'flex',
                        justifyContent: 'center',
                        alignItems: 'center',
                    }}>
                        <IconButton color={'error'} sx={{width: '50%', borderRadius: 0}} onClick={() => onDelete(transactionCategory.transactionCategoryId)}>
                            <DeleteTwoTone fontSize={'large'} />
                        </IconButton>
                        <IconButton color={'inherit'} sx={{width: '50%', borderRadius: 0}} onClick={() => onExit()}>
                            <Clear fontSize={'large'} />
                        </IconButton>
                    </Box>
                </Form>
            )}
        </Formik>
    );
}