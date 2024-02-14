import {AddOutcomeTransactionCommand} from "../../../../models/requests/addOutcomeTransactionCommand";
import {Account} from "../../../../models/accounts/account";
import React, {useState} from "react";
import {
    Button,
    FormControl,
    IconButton,
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
import {Cancel} from "@mui/icons-material";
import {DateTimePicker, LocalizationProvider, MobileDateTimePicker} from "@mui/x-date-pickers";
import {AdapterDayjs} from "@mui/x-date-pickers/AdapterDayjs";
import dayjs from "dayjs";
import MyDateTimePicker from "../../../../components/MyDateTimePicker";
import ValidationConstants from "../../../../utils/constants/validationConstants";

interface AddOutcomeFormProps {
    recipients: string[];
    handleFormSubmit: (values: AddOutcomeTransactionCommand) => Promise<void>;
    accounts: Account[];
    categories: string[]
}

export function AddOutcomeForm({recipients, handleFormSubmit, accounts, categories}: AddOutcomeFormProps) {
    const [newCategoryMode, setNewCategoryMode] = useState(false)
    const [newRecipientMode, setNewRecipientMode] = useState(false)
    const isMobile = useMediaQuery(theme.breakpoints.down('md'))

    const validationSchema = Yup.object({
        transactionDateTime: Yup
            .date()
            .required("Date of transaction is required")
            .max(new Date(Number(new Date()) + 10), 'Select past date'),
        categoryValue: Yup
            .string()
            .required('Category is required')
            .max(30, 'Transaction category name must be between 1 and 30 characters')
            .matches(ValidationConstants.noSpecialCharactersPattern, 'Special characters are not allowed in Transaction category name'),
        transactionAmount: Yup
            .number()
            .required("Amount of transaction is required")
            .positive('Transaction amount must be positive')
            .lessThan(decimal_MAX, 'Transaction amount is too large'),
        sourceAccountId: Yup
            .string()
            .required("Please specify the account sending the transaction"),
        recipientName: Yup
            .string()
            .required('Recipient is required')
            .max(30, 'Transaction entity name must be between 1 and 30 characters')
            .matches(ValidationConstants.noSpecialCharactersPattern, 'Special characters are not allowed in transaction entity name')
    })

    function submit(values: AddOutcomeTransactionCommand, resetForm: () => void) {
        handleFormSubmit(values);
        resetForm();
        setNewCategoryMode(false)
        setNewRecipientMode(false)
    }

    return (
        <Formik
            validationSchema={validationSchema}
            initialValues={new AddOutcomeTransactionCommand()}
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
                        <InputLabel>Account</InputLabel>
                        <Select
                            name='sourceAccountId'
                            value={values.sourceAccountId}
                            onChange={handleChange}
                            label='Account'
                        >
                            {accounts.map(account => (
                                <MenuItem key={account.accountId} value={account.accountId}>{account.name}</MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    {newRecipientMode ? (
                        <MyTextInput
                            placeholder='Recipient'
                            name='recipientName'
                            style={{ minWidth: '60%', maxWidth: '400px' }}
                            inputProps={{ endAdornment: <IconButton onClick={() => setNewRecipientMode(false)}><Cancel /></IconButton> }}
                        />
                    ) : (
                        <FormControl sx={{ minWidth: '60%', maxWidth: '400px' }}>
                            <InputLabel>Recipient</InputLabel>
                            <Select
                                name='recipientName'
                                value={values.recipientName}
                                onChange={handleChange}
                                label='Recipient'
                            >
                                {recipients.map((recipientName, index) => (
                                    <MenuItem key={index} value={recipientName}>{recipientName}</MenuItem>
                                ))}
                                <MenuItem key='newRecipient' value='' onClick={() => setNewRecipientMode(true)}>New Recipient</MenuItem>
                            </Select>
                        </FormControl>
                    )}
                    {newCategoryMode ? (
                        <MyTextInput
                            placeholder='Category'
                            name='categoryValue'
                            style={{ minWidth: '60%', maxWidth: '400px' }}
                            inputProps={{ endAdornment: <IconButton onClick={() => setNewCategoryMode(false)}><Cancel /></IconButton> }}
                        />
                    ) : (
                        <FormControl sx={{ minWidth: '60%', maxWidth: '400px' }}>
                            <InputLabel>Category</InputLabel>
                            <Select
                                name='categoryValue'
                                value={values.categoryValue}
                                onChange={handleChange}
                                label='Category'
                            >
                                {categories.map((categoryValue, index) => (
                                    <MenuItem key={index} value={categoryValue}>{categoryValue}</MenuItem>
                                ))}
                                <MenuItem key='newCategory' value='' onClick={() => setNewCategoryMode(true)}>New Category</MenuItem>
                            </Select>
                        </FormControl>
                    )}
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
                        inputProps={{ endAdornment: <InputAdornment position='end'>USD</InputAdornment>}} // TODO: Fetch user currency
                    />
                    <Button
                        variant='contained'
                        disabled={!isValid}
                        onClick={() => submit(values, resetForm)}
                    >
                        Add Outcome
                    </Button>
                </Form>
            )}
        </Formik>
    )
}