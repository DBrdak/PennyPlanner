import {Account} from "../../../../models/accounts/account";
import {AddInternalTransactionCommand} from "../../../../models/requests/addInternalTransactionCommand";
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
import {DateTimePicker, LocalizationProvider, MobileDateTimePicker} from "@mui/x-date-pickers";
import {AdapterDayjs} from "@mui/x-date-pickers/AdapterDayjs";
import dayjs from "dayjs";

interface AddInternalTransactionFormProps {
    handleFormSubmit: (values: AddInternalTransactionCommand) => Promise<void>
    accounts: Account[]
}

export function AddInternalTransactionForm({accounts, handleFormSubmit}: AddInternalTransactionFormProps) {
    const isMobile = useMediaQuery(theme.breakpoints.down('md'))

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
                    justifyContent: 'space-evenly',
                    alignItems: 'center',
                    flexDirection: 'column',
                    gap: 20
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
                    <LocalizationProvider dateAdapter={AdapterDayjs}>
                        {isMobile
                            ? <MobileDateTimePicker
                                name={'transactionDateTime'}
                                label="Transaction Date Time"
                                value={dayjs(values.transactionDateTime)}
                                onChange={(value) => setValues({...values, transactionDateTime: value ? value.toDate() : new Date()})}
                                sx={{minWidth: '60%', maxWidth: '400px'}}
                            />
                            : <DateTimePicker
                                maxDateTime={dayjs(new Date())}
                                name={'transactionDateTime'}
                                label="Transaction Date Time"
                                value={dayjs(values.transactionDateTime)}
                                onChange={(value) => setValues({...values, transactionDateTime: value ? value.toDate() : new Date()})}
                                sx={{minWidth: '60%', maxWidth: '400px'}}
                            />
                        }
                    </LocalizationProvider>
                    <MyTextInput
                        type='number'
                        name='transactionAmount'
                        placeholder='Amount'
                        maxValue={decimal_MAX}
                        minValue={0}
                        showErrors
                        style={{ minWidth: '60%', maxWidth: '400px' }}
                        inputProps={{ endAdornment: <InputAdornment position='end'>USD</InputAdornment>}} // TODO: Fetch user currency
                    />
                    <Button
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
}