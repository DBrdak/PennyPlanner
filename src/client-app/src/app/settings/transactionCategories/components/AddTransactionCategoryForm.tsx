import {Form, Formik} from "formik";
import MyTextInput from "../../../../components/MyTextInput";
import {Box, IconButton, Typography} from "@mui/material";
import {Clear, DeleteTwoTone, DoneTwoTone} from "@mui/icons-material";
import theme from "../../../theme";
import React from "react";
import * as yup from 'yup'
import {AddTransactionCategoryCommand} from "../../../../models/requests/addTransactionCategoryCommand";
import ValidationConstants from "../../../../utils/constants/validationConstants";

interface TransactionCategoryForm {
    type: "income" | "outcome";
    onSubmit: (command: AddTransactionCategoryCommand) => void
    onExit: () => void
}

export function AddTransactionCategoryForm({type, onSubmit, onExit}: TransactionCategoryForm) {

    const validationSchema = yup.object({
        value: yup.string()
            .required('Value is required')
            .max(30, 'Transaction category value must be between 1 and 30 characters')
            .matches(ValidationConstants.noSpecialCharactersPattern, 'Special characters are not allowed in transaction category value')
    })

    const capitalizedType = type.charAt(0).toUpperCase().concat(type.slice(1))

    return (
        <Formik initialValues={new AddTransactionCategoryCommand(type)} onSubmit={onSubmit} validationSchema={validationSchema}>
            {({handleSubmit, setValues, resetForm, values, isValid}) => (
                <Form style={{}} className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                    <MyTextInput
                        name={'value'}
                        placeholder={'Name'}
                        type={'text'}
                        showErrors
                    />
                    <Typography variant={'subtitle1'} textAlign={'center'} color={
                        type === 'income' ? 'primary' : 'secondary'
                    }>
                        {capitalizedType}
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
                    </Box>
                </Form>
            )}
        </Formik>
    );
}