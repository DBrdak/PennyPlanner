import {Account} from "../../../../models/accounts/account";
import {AddInternalTransactionCommand} from "../../../../models/requests/transactions/addInternalTransactionCommand";
import React from "react";
import {
    Button,
    FormControl,
    InputAdornment,
    InputLabel,
    MenuItem,
    Select,
    useMediaQuery
} from "@mui/material";
import theme from "../../../theme";
import * as Yup from "yup";
import {decimal_MAX} from "../../../../utils/constants/numeric";
import {Form, Formik} from "formik";
import MyTextInput from "../../../../components/MyTextInput";
import MyDateTimePicker from "../../../../components/MyDateTimePicker";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../../stores/store";

interface AddInternalTransactionFormProps {
    handleFormSubmit: (values: AddInternalTransactionCommand) => Promise<void>
    accounts: Account[]
}

export default observer (function AddInternalTransactionForm({accounts, handleFormSubmit}: AddInternalTransactionFormProps) {
    const isMobile = useMediaQuery(theme.breakpoints.down('md'))
    const {userStore} = useStore()

    const validationSchema = Yup.object({
        transactionDateTime: Yup
            .date()
            .required("Date of transaction is required")
            .max(new Date(Number(new Date()) + 10), 'Select past date'),
        transactionAmount: Yup
            .number()
            .required("Amount of transaction is required")
            .positive('Transaction amount must be positive')
            .lessThan(decimal_MAX, 'Transaction amount is too large'),
        fromAccountId: Yup
            .string()
            .required("Please specify the account sending the transaction"),
        toAccountId: Yup
            .string()
            .required("Please specify the account reciving the transaction"),
    })

    function submit(values: AddInternalTransactionCommand, resetForm: () => void) {
        handleFormSubmit(values)
        resetForm()
    }

    return (
        <Formik
            validationSchema={validationSchema}
            initialValues={new AddInternalTransactionCommand()}
            onSubmit={handleFormSubmit}
            validateOnMount
        >
            {({ values, setValues, resetForm, handleChange, isValid }) => (
                <Form autoComplete='off' style={{
                    width: '100%',
                    height: '100%',
                    display: 'flex',
                    justifyContent: 'center',
                    alignItems: 'center',
                    flexDirection: 'column',
                    gap: theme.spacing(5)
                }}>
                    <FormControl sx={{ minWidth: '60%', maxWidth: '400px' }}>
                        <InputLabel>From Account</InputLabel>
                        <Select
                            name='fromAccountId'
                            value={values.fromAccountId}
                            onChange={handleChange}
                            label='From Account'
                        >
                            {accounts.filter(a => a.accountId !== values.toAccountId).map(account => (
                                <MenuItem key={account.accountId} value={account.accountId}>{account.name}</MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    <FormControl sx={{ minWidth: '60%', maxWidth: '400px' }}>
                        <InputLabel>To Account</InputLabel>
                        <Select
                            name='toAccountId'
                            value={values.toAccountId}
                            onChange={handleChange}
                            label='To Account'
                        >
                            {accounts.filter(a => a.accountId !== values.fromAccountId).map(account => (
                                <MenuItem key={account.accountId} value={account.accountId}>{account.name}</MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    <MyDateTimePicker
                        name={'transactionDateTime'}
                        label="Transaction Date Time"
                        maxDateTime={new Date()}
                        isMobile={isMobile}
                        values={values}
                        setValues={setValues}
                        sx={{minWidth: '60%', maxWidth: '400px'}}
                    />
                    <MyTextInput
                        type='number'
                        name='transactionAmount'
                        placeholder='Amount'
                        maxValue={decimal_MAX}
                        minValue={0}
                        showErrors
                        style={{ minWidth: '60%', maxWidth: '400px' }}
                        inputProps={{ endAdornment: <InputAdornment position='end'>{userStore.currentUser?.currency || 'USD'}</InputAdornment>}}
                    />
                    <Button
                        sx={{minWidth: '60%', maxWidth: '400px', borderRadius: '5px'}}
                        variant='contained'
                        disabled={!isValid}
                        onClick={() => submit(values, resetForm)}
                    >
                        Add Transaction
                    </Button>
                </Form>
            )}
        </Formik>
    )
})