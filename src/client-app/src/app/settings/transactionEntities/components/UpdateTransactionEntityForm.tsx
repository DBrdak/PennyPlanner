import {TransactionEntity} from "../../../../models/transactionEntities/transactionEntity";
import {Form, Formik, FormikValues} from "formik";
import {NewAccountData} from "../../../../models/requests/accounts/newAccountData";
import React from "react";
import * as yup from 'yup'
import ValidationConstants from "../../../../utils/constants/validationConstants";
import {Box, IconButton, Typography} from "@mui/material";
import theme from "../../../theme";
import {Clear, DeleteTwoTone, DoneTwoTone, EditTwoTone} from "@mui/icons-material";
import MyTextInput from "../../../../components/MyTextInput";
import ConfirmModal from "../../../../components/ConfirmModal";

interface UpdateTransactionEntityFormProps {
    onDelete: (transactionEntityId: string) => void
    onExit: () => void
    onSubmit: (transactionEntityId: string, newName: string) => void
    transactionEntity: TransactionEntity
}

export function UpdateTransactionEntityForm({transactionEntity, onExit, onDelete, onSubmit}: UpdateTransactionEntityFormProps) {

    const validationSchema = yup.object({
        newName: yup.string()
            .required('Name is required')
            .max(30, 'Transaction entity name must be between 1 and 30 characters')
            .matches(ValidationConstants.noSpecialCharactersPattern, 'Special characters are not allowed in transaction entity name')
            .notOneOf([transactionEntity.name], "New name must differ from current name")
    })

    return (
        <Formik
            initialValues={{newName: transactionEntity.name}}
            onSubmit={(values) => onSubmit(transactionEntity.transactionEntityId, values.newName)}
            validationSchema={validationSchema}
            validateOnMount
        >
            {({handleSubmit, setValues, values, isValid, handleChange}) => (
                <Form style={{}} className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                    <MyTextInput
                        name={'newName'}
                        placeholder={'New Name'}
                        type={'text'}
                        showErrors
                        inputProps={{endAdornment:
                            <IconButton
                                disabled={!isValid}
                                sx={{borderRadius: 0}}
                                color={'success'}
                                onClick={() => {
                                    onExit()
                                    handleSubmit()
                                }}
                            >
                                <DoneTwoTone fontSize={'large'} />
                            </IconButton>
                            }}
                    />
                    <Typography variant={'subtitle1'} textAlign={'center'} color={
                        transactionEntity.transactionEntityType.toLowerCase() === 'sender' ? 'primary' : 'secondary'
                    }>
                        {transactionEntity.transactionEntityType}
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
                        <IconButton color={'error'} sx={{width: '50%', borderRadius: 0}} onClick={() => onDelete(transactionEntity.transactionEntityId)}>
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