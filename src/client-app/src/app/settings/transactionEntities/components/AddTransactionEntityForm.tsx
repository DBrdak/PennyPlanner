import {Form, Formik} from "formik";
import MyTextInput from "../../../../components/MyTextInput";
import {Box, IconButton, Typography} from "@mui/material";
import {Clear, DeleteTwoTone, DoneTwoTone} from "@mui/icons-material";
import theme from "../../../theme";
import React from "react";
import * as yup from 'yup'
import {AddTransactionEntityCommand} from "../../../../models/requests/transactionEntities/addTransactionEntityCommand";
import ValidationConstants from "../../../../utils/constants/validationConstants";

interface TransactionEntityForm {
    type: "sender" | "recipient";
    onSubmit: (command: AddTransactionEntityCommand) => void
    onExit: () => void
}

export function AddTransactionEntityForm({type, onSubmit, onExit}: TransactionEntityForm) {

    const validationSchema = yup.object({
        name: yup.string()
            .required('Name is required')
            .max(30, 'Transaction entity name must be between 1 and 30 characters')
            .matches(ValidationConstants.noSpecialCharactersPattern, 'Special characters are not allowed in transaction entity name')
    })

    const capitalizedType = type.charAt(0).toUpperCase().concat(type.slice(1))

    return (
        <Formik initialValues={new AddTransactionEntityCommand(type)} onSubmit={onSubmit} validationSchema={validationSchema}>
            {({handleSubmit, setValues, resetForm, values, isValid}) => (
                <Form style={{}} className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                    <MyTextInput
                        name={'name'}
                        placeholder={'Name'}
                        type={'text'}
                        showErrors
                    />
                    <Typography variant={'subtitle1'} textAlign={'center'} color={
                        type === 'sender' ? 'primary' : 'secondary'
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