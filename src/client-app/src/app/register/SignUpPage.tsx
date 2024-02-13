import {
    Button,
    Divider,
    FormControl, Grid,
    InputLabel, MenuItem,
    Paper,
    Select,
    Stack,
    Typography
} from "@mui/material";
import CenteredStack from "../../components/CenteredStack";
import React from "react";
import {useNavigate} from "react-router-dom";
import {Form, Formik} from "formik";
import * as Yup from "yup";
import MyTextInput from "../../components/MyTextInput";
import {SignupValues} from "../../models/forms/signUpValues";
import useTitle from "../../utils/hooks/useTitle";
import GradientContainer from "../welcome/GradientContainer";



const SignUpPage: React.FC = () => {
    const navigate = useNavigate()

    useTitle('Register')

    const initialValues: SignupValues = {
        email: '',
        password: '',
        confirmPassword: '',
        name: '',
        currency: 'USD'
    }


    const passwordManual =
        `Your password should contain: 
    · 8 characters 
    · 1 special character 
    · 1 lower case letter 
    · 1 upper case letter 
    · 1 digit`

    const validationSchema = Yup.object({
        email: Yup.string()
            .matches(RegExp(`^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$`), {message: 'Sorry, we cannot contact with this email'})
            .required('How can we contact with you?'),
        password: Yup.string()
            .required('Protect your account')
            .matches(RegExp(`^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[!@#$%^&*()-_=+{};:'",<.>/?\\\\[\\]_\`|~])(?!.*\\s).{8,}$`),
                {message: 'This password is too weak'}),
        confirmPassword: Yup.string()
            .oneOf([Yup.ref('password')], 'Passwords differ')
            .required('Confirm your password'),
        currency: Yup.string()
            .required('Provide currency which you will use'),
        name: Yup.string()
            .required('How can we call you?')
            .max(30, 'Use shorter name')
    });

    function handleFormSubmit(values: any) {
        return undefined;
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
                <Stack justifyContent={'center'} width={'100%'} spacing={3}>
                    <Formik
                        validationSchema={validationSchema}
                        initialValues={initialValues}
                        onSubmit={values => handleFormSubmit(values)}
                    >
                        {({ handleSubmit, setValues, values}) => (
                            <Form className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                                <Grid container>
                                    <Grid xs={12} md={6} sx={{
                                        padding: 2
                                    }}>
                                        <MyTextInput
                                            name={'name'}
                                            label="Name"
                                            placeholder="Name"
                                            type={'text'}
                                            showErrors
                                        />
                                    </Grid>
                                    <Grid xs={12} md={6} sx={{
                                        padding: 2
                                    }}>
                                        <MyTextInput
                                            name={'email'}
                                            label="Email"
                                            placeholder="Email"
                                            type="email"
                                            showErrors
                                        />
                                    </Grid>
                                    <Grid xs={12} md={6} sx={{
                                        padding: 2
                                    }}>
                                        <MyTextInput
                                            name={'password'}
                                            label="Password"
                                            placeholder="Password"
                                            type="password"
                                            showErrors
                                        />
                                    </Grid>
                                    <Grid xs={12} md={6} sx={{
                                        padding: 2
                                    }}>
                                        <MyTextInput
                                            name={'confirmPassword'}
                                            label="Confirm password"
                                            placeholder="Confirm password"
                                            type="password"
                                            showErrors
                                        />
                                    </Grid>
                                    <Grid xs={12} md={6} sx={{
                                        padding: 2
                                    }}>
                                        <FormControl fullWidth error={values.currency.length < 1}>
                                            <InputLabel>Currency</InputLabel>
                                            <Select
                                                value={values.currency}
                                                onChange={(e) => setValues({...values, currency: e.target.value})}
                                            >
                                                <MenuItem value={'PLN'}>PLN</MenuItem>
                                                <MenuItem value={'USD'}>USD</MenuItem>
                                            </Select>
                                        </FormControl>
                                    </Grid>
                                    <Grid xs={12} md={6} sx={{
                                        display: 'flex',
                                        justifyContent: 'center',
                                        alignItems: 'center',
                                        padding: 2
                                    }}>
                                        <Button fullWidth type="submit" variant="contained" color="primary" onClick={() => console.log('Registered !')}>
                                            Sign Up
                                        </Button>
                                    </Grid>
                                </Grid>
                            </Form>
                        )}
                    </Formik>
                    <Typography variant={'caption'} textAlign={'center'} style={{whiteSpace: 'pre-wrap'}}>
                        {passwordManual}
                    </Typography>
                    <Divider variant="middle" />
                    <CenteredStack>
                        <Typography variant="body2" color="textSecondary" align="center" style={{ margin: '10px 0' }}>
                            Already have an account?
                        </Typography>
                        <Button variant={"outlined"} color={'secondary'} style={{width: '75%', borderWidth: '3px', fontWeight: '900'}}
                                onClick={() => navigate('/login')}>
                            Sign In
                        </Button>
                    </CenteredStack>
                </Stack>
            </Paper>
        </GradientContainer>
    )
}

export default SignUpPage