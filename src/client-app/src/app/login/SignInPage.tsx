import React, {useEffect} from 'react';
import {
    Paper,
    Typography,
    Button,
    TextField,
    Divider,
    Stack,
    ButtonGroup, InputAdornment, IconButton, Grid, CircularProgress, useMediaQuery
} from '@mui/material';
import {Form, Formik, useFormik} from 'formik';
import * as Yup from 'yup';
import CenteredStack from "../../components/CenteredStack";
import {router} from "../../router/Routes";
import {useNavigate} from "react-router-dom";
import useTitle from "../../utils/hooks/useTitle";
import GradientContainer from "../welcome/GradientContainer";
import {LogInUserCommand} from "../../models/requests/users/logInUserCommand";
import {DeleteTwoTone, Undo, Visibility, VisibilityOff} from "@mui/icons-material";
import theme from "../theme";
import MyTextInput from "../../components/MyTextInput";
import GroupDropdown from "../../components/transactionsTable/GroupDropdown";
import {useStore} from "../../stores/store";
import {toast} from "react-toastify";
import {observer} from "mobx-react-lite";

const SignInPage: React.FC = () => {
    const navigate = useNavigate()
    const isMobile = useMediaQuery(theme.breakpoints.down('md'))
    const [showPassword, setShowPassword] = React.useState(false)
    const {userStore} = useStore()

    useTitle('Login')

    useEffect(() => {
    }, [userStore.loading])

    const validationSchema = Yup.object({
        email: Yup.string().email('Invalid email address').required('Insert email'),
        password: Yup.string().required('Insert password'),
    });

    const initialValues = new LogInUserCommand()

    const handleLogIn = (command: LogInUserCommand) => {
        userStore.logIn(command).then(() => userStore.currentUser && navigate('/home'))
    }

    return (
        <GradientContainer sx={{userSelect: 'none'}}>
            <Paper elevation={3} style={{
                borderRadius: '70px',
                minHeight: '600px',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                maxWidth: '650px',
            }}
            >
                    <Formik sx={{width: '100%'}} initialValues={initialValues} onSubmit={handleLogIn} validationSchema={validationSchema}>
                        {({setValues, values, isValid}) => (
                            <Form autoComplete='off'
                                  style={{
                                      width: '100%',
                                      display: 'flex',
                                      justifyContent: 'center',
                                      alignItems: 'center',
                                      flexDirection: 'column',
                                      position: 'relative'
                                  }}>
                                <Grid container width={'100%'} sx={{
                                    display: 'flex', justifyContent: 'center',
                                }}>
                                    <Grid item xs={12} sx={{
                                        display: 'flex', justifyContent: 'center',
                                        marginBottom: '20px'
                                    }}>
                                        <MyTextInput
                                            style={{width: '70%', minWidth: '300px'}}
                                            name={'email'}
                                            placeholder="Email"
                                            type="email"
                                            showErrors
                                        />
                                    </Grid>
                                    <Grid item xs={12} sx={{
                                        display: 'flex', justifyContent: 'center',
                                        marginBottom: '20px'
                                    }}>
                                        <MyTextInput
                                            style={{width: '70%', minWidth: '300px'}}
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
                                        display: 'flex', justifyContent: 'center',
                                        marginBottom: '20px'
                                    }}>
                                        <Button type="submit" variant="contained" color="primary" style={{ width: '45%', minWidth: '200px' }}>
                                            {
                                                userStore.loading
                                                    ? <CircularProgress color={'inherit'} />
                                                    : 'Sign In'
                                            }
                                        </Button>
                                    </Grid>

                                    <Grid item xs={8}>
                                        <Divider variant="middle" />
                                    </Grid>

                                    <Grid item xs={12} sx={{
                                        display: 'flex', justifyContent: 'center', alignItems:'center', flexDirection: 'column',
                                        marginBottom: '20px'
                                    }}>
                                        <Typography variant="body2" color="textSecondary" align="center" style={{ margin: '10px 0' }}>
                                            Available soon
                                        </Typography>
                                        <ButtonGroup orientation={isMobile ? 'vertical' : 'horizontal'} sx={{borderRadius: 0}}>
                                            <Button disabled
                                                    variant="outlined"
                                                    fullWidth
                                                    color={'inherit'}
                                                    style={{textWrap: 'nowrap', padding: '15px', borderRadius: 0}}
                                                    endIcon={<img src="/assets/FB_Logo.png" alt="Facebook Logo" style={{ width: '20px', marginRight: '10px' }} />}
                                            >
                                                Sign in with Facebook
                                            </Button>
                                            <Button disabled
                                                    variant="outlined"
                                                    fullWidth
                                                    color={'inherit'}
                                                    style={{textWrap: 'nowrap', padding: '15px', borderRadius: 0}}
                                                    startIcon={<img src="/assets/Google_Logo.png" alt="Google Logo" style={{ width: '20px', marginRight: '10px' }} />}
                                            >
                                                Sign in with Google
                                            </Button>
                                        </ButtonGroup>
                                    </Grid>

                                    <Grid item xs={8}>
                                        <Divider variant="middle" />
                                    </Grid>

                                    <Grid item xs={12} sx={{
                                        display: 'flex', justifyContent: 'center', alignItems:'center', flexDirection: 'column'
                                    }}>
                                        <Typography variant="body2" color="textSecondary" align="center" style={{ margin: '10px 0' }}>
                                            Don't have an account?
                                        </Typography>
                                        <Button variant={"outlined"} color={'secondary'} style={{width: '45%', minWidth: '200px', borderWidth: '3px', fontWeight: '900'}}
                                                onClick={() => navigate('/register')}>
                                            Sign up
                                        </Button>
                                    </Grid>
                                </Grid>
                            </Form>
                        )}
                    </Formik>
            </Paper>
        </GradientContainer>
    );
};

export default observer(SignInPage)
