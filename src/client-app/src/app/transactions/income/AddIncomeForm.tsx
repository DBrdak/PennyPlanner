import {Form, Formik} from "formik";
import {AddIncomeTransactionCommand} from "../../../models/requests/addIncomeTransactionCommand";
import {
    Autocomplete,
    Button,
    FormControl, IconButton,
    InputAdornment,
    InputLabel,
    MenuItem,
    Select,
    TextField,
    useMediaQuery
} from "@mui/material";
import * as Yup from "yup";
import {TransactionCategory} from "../../../models/transactionCategories/transactionCategory";
import {TransactionEntity} from "../../../models/transactionEntities/transactionEntity";
import {Account} from "../../../models/accounts/account";
import {AdapterDayjs} from "@mui/x-date-pickers/AdapterDayjs";
import {DateTimePicker, LocalizationProvider, MobileDateTimePicker} from "@mui//x-date-pickers";
import dayjs from "dayjs";
import theme from "../../theme";
import MyTextInput from "../../../components/MyTextInput";
import React, {useState} from "react";
import {Cancel} from "@mui/icons-material";

interface AddIncomeFormProps {
    accounts: Account[]
    categories: TransactionCategory[]
    senders: TransactionEntity[]
    handleFormSubmit: (values: AddIncomeTransactionCommand) => void
}

export function AddIncomeForm({ accounts, categories, senders, handleFormSubmit }: AddIncomeFormProps) {
    const [newCategoryMode, setNewCategoryMode] = useState(false)
    const [newSenderMode, setNewSenderMode] = useState(false)
    const isMobile = useMediaQuery(theme.breakpoints.down('md'))

    const validationSchema = Yup.object({
        destinationAccountId: Yup
            .string()
            .required("Please specify the account receiving the transaction"),
        senderName: Yup
            .string()
            .required("Please specify the entity sending the transaction"),
        transactionAmount: Yup
            .number()
            .required("Amount of transaction is required")
            .positive("Amount of transaction must be grater than zero"),
        category: Yup
            .string()
            .required("Category of transaction is required"),
        transactionDateTime: Yup
            .date()
            .required("Date of transaction is required")
            .max(new Date(), 'Select past date')
    })

    return (
        <Formik
            validationSchema={validationSchema}
            initialValues={new AddIncomeTransactionCommand()}
            onSubmit={handleFormSubmit}
        >
            {({ values, resetForm, handleChange, isValid }) => (
                <Form autoComplete='off' style={{ width: '100%', display: 'flex', justifyContent: 'center', alignItems: 'center', flexDirection: 'column', gap: 10 }}>
                    <FormControl sx={{ minWidth: '30%', maxWidth: '400px' }}>
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
                            style={{ minWidth: '30%', maxWidth: '400px' }}
                            inputProps={{ endAdornment: <IconButton onClick={() => setNewSenderMode(false)}><Cancel /></IconButton> }}
                        />
                    ) : (
                        <FormControl sx={{ minWidth: '30%', maxWidth: '400px' }}>
                            <InputLabel>Sender</InputLabel>
                            <Select
                                name='senderName'
                                value={values.senderName}
                                onChange={handleChange}
                                label='Sender'
                            >
                                {senders.map((sender, index) => (
                                    <MenuItem key={index} value={sender.name}>{sender.name}</MenuItem>
                                ))}
                                <MenuItem key='newSender' value='' onClick={() => setNewSenderMode(true)}>New Sender</MenuItem>
                            </Select>
                        </FormControl>
                    )}
                    {newCategoryMode ? (
                        <MyTextInput
                            placeholder='Category'
                            name='category'
                            style={{ minWidth: '30%', maxWidth: '400px' }}
                            inputProps={{ endAdornment: <IconButton onClick={() => setNewCategoryMode(false)}><Cancel /></IconButton> }}
                        />
                    ) : (
                        <FormControl sx={{ minWidth: '30%', maxWidth: '400px' }}>
                            <InputLabel>Category</InputLabel>
                            <Select
                                name='category'
                                value={values.category}
                                onChange={handleChange}
                                label='Category'
                            >
                                {categories.map((category, index) => (
                                    <MenuItem key={index} value={category.value}>{category.value}</MenuItem>
                                ))}
                                <MenuItem key='newCategory' value='' onClick={() => setNewCategoryMode(true)}>New Category</MenuItem>
                            </Select>
                        </FormControl>
                    )}
                    <LocalizationProvider dateAdapter={AdapterDayjs}>
                        {isMobile
                            ? <MobileDateTimePicker
                                name={'transactionDateTime'}
                                label="Transaction Date Time"
                                value={dayjs(values.transactionDateTime)}
                                onChange={handleChange}
                                sx={{minWidth: '30%', maxWidth: '400px'}}
                            />
                            : <DateTimePicker
                                name={'transactionDateTime'}
                                label="Transaction Date Time"
                                value={dayjs(values.transactionDateTime)}
                                onChange={handleChange}
                                sx={{minWidth: '30%', maxWidth: '400px'}}
                            />
                        }
                    </LocalizationProvider>
                    <MyTextInput
                        type='number'
                        name='transactionAmount'
                        placeholder='Amount'
                        style={{ minWidth: '30%', maxWidth: '400px' }}
                        inputProps={{ endAdornment: <InputAdornment position='end'>USD</InputAdornment> }} // TODO: Fetch user currency
                    />
                    <Button
                        variant='contained'
                        disabled={!isValid}
                        onClick={() => {
                            console.log(values)
                            handleFormSubmit(values);
                            resetForm();
                        }}
                    >
                        Add Transaction
                    </Button>
                </Form>
            )}
        </Formik>
    )
}