import {Form, Formik} from "formik";
import {AddIncomeTransactionCommand} from "../../../../models/requests/transactions/addIncomeTransactionCommand";
import {
    Button,
    FormControl, IconButton,
    InputAdornment,
    InputLabel,
    MenuItem,
    Select,
    useMediaQuery
} from "@mui/material";
import * as Yup from "yup";
import {Account} from "../../../../models/accounts/account";
import theme from "../../../theme";
import MyTextInput from "../../../../components/MyTextInput";
import React, {useState} from "react";
import {Cancel} from "@mui/icons-material";
import {decimal_MAX} from "../../../../utils/constants/numeric";
import MyDateTimePicker from "../../../../components/MyDateTimePicker";
import ValidationConstants from "../../../../utils/constants/validationConstants";

interface AddIncomeFormProps {
    accounts: Account[]
    categories: string[]
    senders: string[]
    subcategories: string[]
    handleFormSubmit: (values: AddIncomeTransactionCommand) => void
}

export function AddIncomeForm({ accounts, categories, senders, subcategories, handleFormSubmit }: AddIncomeFormProps) {
    const [newCategoryMode, setNewCategoryMode] = useState(false)
    const [newSubcategoryMode, setNewSubcategoryMode] = useState(false)
    const [newSenderMode, setNewSenderMode] = useState(false)
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
        subcategoryValue: Yup
            .string()
            .required('Subcategory is required')
            .max(30, 'Transaction subcategory name must be between 1 and 30 characters')
            .matches(ValidationConstants.noSpecialCharactersPattern, 'Special characters are not allowed in Transaction subcategory name'),
        transactionAmount: Yup
            .number()
            .required("Amount of transaction is required")
            .positive('Transaction amount must be positive')
            .lessThan(decimal_MAX, 'Transaction amount is too large'),
        destinationAccountId: Yup
            .string()
            .required("Please specify the account receiving the transaction"),
        senderName: Yup
            .string()
            .required('Sender is required')
            .max(30, 'Transaction entity name must be between 1 and 30 characters')
            .matches(ValidationConstants.noSpecialCharactersPattern, 'Special characters are not allowed in transaction entity name')
    })

    function submit(values: AddIncomeTransactionCommand, resetForm: () => void) {
        handleFormSubmit(values);
        resetForm();
        setNewCategoryMode(false)
        setNewSenderMode(false)
    }

    return (
        <Formik
            validationSchema={validationSchema}
            initialValues={new AddIncomeTransactionCommand()}
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
                        <InputLabel>Account</InputLabel>
                        <Select
                            name='destinationAccountId'
                            value={values.destinationAccountId}
                            onChange={handleChange}
                            label='Account'
                        >
                            {accounts.map(account => (
                                <MenuItem key={account.accountId} value={account.accountId}>{account.name}</MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    {newSenderMode ? (
                        <MyTextInput
                            placeholder='Sender'
                            name='senderName'
                            style={{ minWidth: '60%', maxWidth: '400px' }}
                            inputProps={{ endAdornment: <IconButton onClick={() => setNewSenderMode(false)}><Cancel /></IconButton> }}
                        />
                    ) : (
                        <FormControl sx={{ minWidth: '60%', maxWidth: '400px' }}>
                            <InputLabel>Sender</InputLabel>
                            <Select
                                name='senderName'
                                value={values.senderName}
                                onChange={handleChange}
                                label='Sender'
                            >
                                {senders.map((senderName, index) => (
                                    <MenuItem key={index} value={senderName}>{senderName}</MenuItem>
                                ))}
                                <MenuItem key='newSender' value='' onClick={() => setNewSenderMode(true)}>New Sender</MenuItem>
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
                    {!values.categoryValue ? <></> :  newSubcategoryMode ? (
                        <MyTextInput
                            placeholder='Subcategory'
                            name='subcategoryValue'
                            style={{ minWidth: '60%', maxWidth: '400px' }}
                            inputProps={{ endAdornment: <IconButton onClick={() => setNewSubcategoryMode(false)}><Cancel /></IconButton> }}
                        />
                    ) : (
                        <FormControl sx={{ minWidth: '60%', maxWidth: '400px' }}>
                            <InputLabel>Subcategory</InputLabel>
                            <Select
                                name='subcategoryValue'
                                value={values.subcategoryValue}
                                onChange={handleChange}
                                label='Subcategory'
                            >
                                {subcategories.map((subcategoryValue, index) => (
                                    <MenuItem key={index} value={subcategoryValue}>{subcategoryValue}</MenuItem>
                                ))}
                                <MenuItem key='newSubcategory' value='' onClick={() => setNewSubcategoryMode(true)}>New Subcategory</MenuItem>
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
                        sx={{minWidth: '60%', maxWidth: '400px', borderRadius: '5px'}}
                        variant='contained'
                        disabled={!isValid}
                        onClick={() => submit(values, resetForm)}
                    >
                        Add Income
                    </Button>
                </Form>
            )}
        </Formik>
    )
}