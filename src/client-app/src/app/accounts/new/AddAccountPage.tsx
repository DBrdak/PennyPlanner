import AppOverlay from "../../../components/appOverlay/AppOverlay";
import {useStore} from "../../../stores/store";
import {observer} from "mobx-react-lite";
import {
    Button,
    ButtonGroup,
    FormControl,
    Grid, InputAdornment,
    InputLabel, MenuItem,
    Select,
    Stack,
    Typography,
    useMediaQuery
} from "@mui/material";
import theme from "../../theme";
import {Form, Formik} from "formik";
import MyTextInput from "../../../components/MyTextInput";
import {UndoTwoTone} from "@mui/icons-material";
import {useNavigate} from "react-router-dom";
import {NewAccountData} from "../../../models/requests/accounts/newAccountData";
import * as yup from 'yup'
import {number, string} from "yup";
import React from "react";
import ValidationConstants from "../../../utils/constants/validationConstants";
import useAuthProvider from "../../../utils/hooks/useAuthProvider";


export default observer(function AddAccountPage() {
    useAuthProvider()
    const navigate = useNavigate()
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
    const isUwhd = useMediaQuery(theme.breakpoints.up('xl'))
    const {accountStore} = useStore()

    const validationSchema = yup.object({
        name: string()
            .required('Pass account name')
            .max(30, 'Account name should be shorter than 30 characters')
            .matches(ValidationConstants.noSpecialCharactersPattern, {message: "Special characters are not allowed in account name"}),
        type: string().required().oneOf(['Transactional', 'Savings'], 'Invalid account type'),
        initialBalance: number().required('Pass initial account balance')
    })

    const handleFormSubmit = async (accountData: NewAccountData) => {
        await accountStore.addAccount(accountData)
        navigate('/accounts')
    }

    return (
        <AppOverlay>
            <Grid container sx={{
                height:'100%',
                padding: isMobile ? 3 : 4,
                margin: 0,
                backgroundColor: theme.palette.background.paper,
                borderRadius: '20px',
                overflow:'auto',
                display:'flex',
                justifyContent: 'center',
                alignItems: 'center',
                maxWidth: '1920px'
            }}>
                <Grid item xs={12} sx={{
                    height: '65%',
                    display:'flex',
                    justifyContent: 'center',
                    alignItems: 'center'
                }}>
                    <Formik initialValues={new NewAccountData()} onSubmit={handleFormSubmit} validationSchema={validationSchema}>
                        {({handleSubmit, setValues, values, handleChange}) => (
                            <Form style={{width: '100%', maxWidth: '400px'}} className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                                <Stack spacing={3}>
                                    <Typography variant={'h3'} sx={{userSelect: 'none', paddingBottom: '3%', textAlign: 'center'}}>
                                        Add new account
                                    </Typography>
                                    <FormControl>
                                        <InputLabel>Account Type</InputLabel>
                                        <Select
                                            id={'type'}
                                            name={'type'}
                                            value={values.type}
                                            label={'Account Type'}
                                            onChange={handleChange}
                                        >
                                            <MenuItem key={1} value={'Transactional'}>Transactional Account</MenuItem>
                                            <MenuItem key={2} value={'Savings'}>Savings Account</MenuItem>
                                        </Select>
                                    </FormControl>
                                    <MyTextInput
                                        name={'name'}
                                        placeholder={'Account Name'}
                                        type={'text'}
                                        showErrors
                                    />
                                    <MyTextInput
                                        name={'initialBalance'}
                                        placeholder={'Initial Account Balance'}
                                        type={'number'}
                                        showErrors
                                        inputProps={{endAdornment: <InputAdornment position="end">USD</InputAdornment>} /*TODO fetch user currency*/}
                                    />
                                    <ButtonGroup sx={{alignItems:'center', justifyContent: 'center'}}>
                                        <Button fullWidth
                                                variant={'outlined'}
                                                color={'error'}
                                                onClick={() => navigate('/accounts')}
                                        >
                                            <UndoTwoTone color={'error'} />
                                        </Button>
                                        <Button fullWidth variant={'contained'} type={'submit'}>
                                            Add
                                        </Button>
                                    </ButtonGroup>
                                </Stack>
                            </Form>
                        )}
                    </Formik>
                </Grid>
            </Grid>
        </AppOverlay>
    );
})