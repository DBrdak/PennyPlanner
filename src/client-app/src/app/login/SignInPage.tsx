import React from 'react';
import {
    Paper,
    Typography,
    Button,
    TextField,
    Divider,
    Stack,
    ButtonGroup, useMediaQuery
} from '@mui/material';
import { useFormik } from 'formik';
import * as Yup from 'yup';
import CenteredStack from "../../components/CenteredStack";
import {router} from "../../router/Routes";
import {useNavigate} from "react-router-dom";
import theme from "../theme";
import useTitle from "../../utils/hooks/useTitle";
import GradientContainer from "../welcome/GradientContainer";

const validationSchema = Yup.object({
    email: Yup.string().email('Invalid email address').required('Insert email'),
    password: Yup.string().required('Insert password'),
});

const SignInPage: React.FC = () => {
    // TODO Implement logging and registering logic
    const navigate = useNavigate()
    const [showPassword, setShowPassword] = React.useState(false);

    useTitle('Login')

    const formik = useFormik({
        initialValues: {
            email: '',
            password: '',
        },
        validationSchema: validationSchema,
        onSubmit: (values) => {
            router.navigate('')
        },
    });

    return (
        <GradientContainer sx={{userSelect: 'none'}}>
            <Stack style={{minWidth: '600px'}}>
                <Typography variant="h4" align="center" gutterBottom>
                    Your Logo
                </Typography>
                <Paper elevation={3} style={{
                    padding: '3rem', borderRadius: '70px', minHeight: '600px', display: 'flex', alignItems: 'center', justifyContent: 'center' }}
                >
                    <Stack justifyContent={'center'} height={'100%'} spacing={3}>
                        <form onSubmit={formik.handleSubmit}>
                            <CenteredStack>
                                <TextField
                                    label="Email"
                                    type="email"
                                    variant="outlined"
                                    fullWidth
                                    margin="normal"
                                    {...formik.getFieldProps('email')}
                                    error={formik.touched.email && Boolean(formik.errors.email)}
                                    helperText={formik.touched.email && formik.errors.email}
                                />

                                <TextField
                                    label="Password"
                                    type={showPassword ? 'text' : "password"}
                                    variant='outlined'
                                    fullWidth
                                    margin="normal"
                                    {...formik.getFieldProps('password')}
                                    error={formik.touched.password && Boolean(formik.errors.password)}
                                    helperText={formik.touched.password && formik.errors.password}
                                />

                                <Button type="submit" variant="contained" color="primary" style={{ marginTop: '20px', width: '75%' }}
                                        onClick={() => console.log('Logged In !')}>
                                    Sign In
                                </Button>
                            </CenteredStack>
                        </form>

                        <Divider variant="middle" />

                        <CenteredStack direction={'row'} style={{width: '100%', gap: 10}}>
                            <Stack>
                                <Typography variant="body2" color="textSecondary" align="center" style={{ margin: '10px 0' }}>
                                    You can also
                                </Typography>
                                <ButtonGroup>
                                    <Button
                                        variant="outlined"
                                        fullWidth
                                        color={'inherit'}
                                        style={{textWrap: 'nowrap', padding: '15px'}}
                                        endIcon={<img src="/assets/FB_Logo.png" alt="Facebook Logo" style={{ width: '20px', marginRight: '10px' }} />}
                                    >
                                        Sign in with Facebook
                                    </Button>
                                    <Button
                                        variant="outlined"
                                        fullWidth
                                        color={'inherit'}
                                        style={{textWrap: 'nowrap', padding: '15px'}}
                                        startIcon={<img src="/assets/Google_Logo.png" alt="Google Logo" style={{ width: '20px', marginRight: '10px' }} />}
                                    >
                                        Sign in with Google
                                    </Button>
                                </ButtonGroup>
                            </Stack>
                        </CenteredStack>

                        <Divider variant="middle" />

                        <CenteredStack>
                            <Typography variant="body2" color="textSecondary" align="center" style={{ margin: '10px 0' }}>
                                Don't have an account?
                            </Typography>
                            <Button variant={"outlined"} color={'secondary'} style={{width: '75%', borderWidth: '3px', fontWeight: '900'}}
                                    onClick={() => navigate('/register')}>
                                Sign up
                            </Button>
                        </CenteredStack>
                    </Stack>
                </Paper>
            </Stack>
        </GradientContainer>
    );
};

export default SignInPage;
