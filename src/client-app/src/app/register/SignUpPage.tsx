import {
    Button, CircularProgress,
    Divider,
    FormControl, Grid, IconButton, InputAdornment,
    InputLabel, MenuItem,
    Paper,
    Select,
    Stack,
    Typography, useMediaQuery
} from "@mui/material";
import CenteredStack from "../../components/CenteredStack";
import React, {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";
import {Form, Formik} from "formik";
import * as Yup from "yup";
import MyTextInput from "../../components/MyTextInput";
import useTitle from "../../utils/hooks/useTitle";
import GradientContainer from "../welcome/GradientContainer";
import {RegisterUserCommand} from "../../models/requests/users/registerUserCommand";
import ValidationConstants from "../../utils/constants/validationConstants";
import {observer} from "mobx-react-lite";
import {useStore} from "../../stores/store";
import {isValidDateValue} from "@testing-library/user-event/dist/utils";
import {toast} from "react-toastify";
import theme from "../theme";
import {Visibility, VisibilityOff} from "@mui/icons-material";



const SignUpPage: React.FC = () => {
    const navigate = useNavigate()
    const {userStore} = useStore()
    const [takenEmails, setTakenEmails] = useState<string[]>([])
    const [showPassword, setShowPassword] = React.useState(false)
    useTitle('Register')

    useEffect(() => {
    }, [userStore.loading])

    const initialValues = new RegisterUserCommand()

    const passwordManual =
        `Your password should contain: 
    · 8 characters 
    · 1 special character 
    · 1 lower case letter 
    · 1 upper case letter 
    · 1 digit`

    const validationSchema = Yup.object({
        email: Yup.string()
            .matches(ValidationConstants.emailPattern, {message: 'Sorry, we cannot contact with this email'})
            .required('How can we contact with you?')
            .notOneOf(takenEmails, 'Email is taken'),
        username: Yup.string()
            .matches(ValidationConstants.noSpecialCharactersPattern)
            .required("How can we call you?"),
        password: Yup.string()
            .required('Protect your account')
            .matches(ValidationConstants.passwordPattern,{message: 'This password is too weak'}),
        currency: Yup.string()
            .required('Provide currency which you will use')
    });

    function handleFormSubmit(values: RegisterUserCommand) {
        userStore.register(values).then(isSuccess => {
            if(userStore.currentUser && isSuccess) navigate('/home')

            else setTakenEmails(prev => [...prev, values.email])
        })
    }

    return (
        <GradientContainer sx={{userSelect: 'none'}}>
            <Paper elevation={3} style={{
                minWidth: '400px',
                maxWidth: '600px',
                padding: '3rem',
                borderRadius: '70px',
                minHeight: '600px',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                margin: '3em 0em',
            }}>
                <Grid container>
                    <Grid item xs={12}>
                        <Formik
                            validationSchema={validationSchema}
                            initialValues={initialValues}
                            onSubmit={values => handleFormSubmit(values)}
                        >
                            {({ handleSubmit, setValues, values, isValid}) => (
                                <Form className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                                    <Grid container width={'100%'}>
                                        <Grid item xs={12} sx={{
                                            marginBottom: '20px',
                                            padding: '0px 20px'
                                        }}>
                                            <MyTextInput
                                                style={{width: '100%'}}
                                                name={'email'}
                                                label="Email"
                                                placeholder="Email"
                                                type="email"
                                                showErrors
                                            />
                                        </Grid>
                                        <Grid item xs={12} sx={{
                                            marginBottom: '20px',
                                            padding: '0px 20px'
                                        }}>
                                            <MyTextInput
                                                style={{width: '100%'}}
                                                name={'username'}
                                                label="Username"
                                                placeholder="Username"
                                                type="text"
                                                showErrors
                                            />
                                        </Grid>
                                        <Grid item xs={12} sx={{
                                            marginBottom: '20px',
                                            padding: '0px 20px'
                                        }}>
                                            <FormControl fullWidth error={values.currency.length < 1}>
                                                <InputLabel>Currency</InputLabel>
                                                <Select
                                                    value={values.currency}
                                                    onChange={(e) => setValues({...values, currency: e.target.value})}
                                                >
                                                    <MenuItem value={'PLN'}>PLN</MenuItem>
                                                    <MenuItem value={'USD'}>USD</MenuItem>
                                                    <MenuItem value={'GBP'}>GBP</MenuItem>
                                                    <MenuItem value={'CAD'}>CAD</MenuItem>
                                                    <MenuItem value={'EUR'}>EUR</MenuItem>
                                                    <MenuItem value={'CHF'}>CHF</MenuItem>
                                                    <MenuItem value={'CHF'}>JPY</MenuItem>
                                                </Select>
                                            </FormControl>
                                        </Grid>
                                        <Grid item xs={12} sx={{
                                            marginBottom: '20px',
                                            padding: '0px 20px'
                                        }}>
                                            <MyTextInput
                                                style={{width: '100%', minWidth: '300px'}}
                                                name={'password'}
                                                placeholder="Password"
                                                type={showPassword ? 'text' : "password"}
                                                inputProps={{endAdornment:
                                                        <InputAdornment position="end">
                                                            <IconButton onClick={() => setShowPassword(prev => !prev)}>
                                                                {showPassword ? <Visibility /> : <VisibilityOff />}
                                                            </IconButton>
                                                        </InputAdornment>}}
                                            />
                                        </Grid>
                                        <Grid item xs={12} sx={{
                                            display: 'flex',
                                            justifyContent: 'center',
                                            alignItems: 'center',
                                            marginBottom: '20px'
                                        }}>
                                            <Button fullWidth disabled={!isValid || takenEmails.some(email => email === values.email)} type="submit" variant="contained" color="primary" sx={{maxWidth: '250px'}}>
                                                {
                                                    userStore.loading
                                                        ? <CircularProgress color={'inherit'} />
                                                        : 'Sign Up'
                                                }
                                            </Button>
                                        </Grid>
                                    </Grid>
                                </Form>
                            )}
                        </Formik>
                    </Grid>
                    <Grid item xs={12} textAlign={'center'} marginBottom={'20px'}>
                        <Typography variant={'caption'} textAlign={'center'} style={{whiteSpace: 'pre-wrap'}}>
                            {passwordManual}
                        </Typography>
                    </Grid>
                    <Grid item xs={12} marginBottom={'20px'}>
                        <Divider variant="middle" />
                    </Grid>
                    <Grid item xs={12}>
                        <CenteredStack>
                            <Typography variant="body2" color="textSecondary" align="center" style={{ margin: '10px 0' }}>
                                Already have an account?
                            </Typography>
                            <Button variant={"outlined"} color={'secondary'} style={{width: '75%', borderWidth: '3px', fontWeight: '900'}}
                                    onClick={() => navigate('/login')}>
                                Sign In
                            </Button>
                        </CenteredStack>
                    </Grid>
                </Grid>
            </Paper>
        </GradientContainer>
    )
}

export default observer(SignUpPage)