import {observer} from "mobx-react-lite";
import {useStore} from "../../stores/store";
import {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";
import GradientContainer from "../welcome/GradientContainer";
import {Button, ButtonGroup, CircularProgress, Typography, useMediaQuery} from "@mui/material";
import theme from "../theme";
import {ArrowRight, InboxTwoTone} from "@mui/icons-material";
import {toast} from "react-toastify";
import useTitle from "../../utils/hooks/useTitle";

export default observer(function EmailVerificationPage() {
    const [timer, setTimer] = useState(0);
    const {userStore} = useStore()
    const navigate = useNavigate()
    const isMobile = useMediaQuery(theme.breakpoints.down('sm'));
    const [requestedForReload, setRequestedForReload] = useState(false)
    useTitle('Verification')

    useEffect(() => {
        if(!userStore.currentUser){
            navigate('/')
        }
        else if(userStore.currentUser.isEmailVerified){
            navigate('/home')
        }
        else if(!requestedForReload && !userStore.currentUser.isEmailVerified) {
            toast.warn('Your email is not verified')
        }
    }, [requestedForReload])

    const headerStyles: React.CSSProperties = {
        color: theme.palette.common.white,
        marginBottom: theme.spacing(3),
        userSelect: 'none',
        textShadow: '2px 2px 6px rgba(0,0,0, 0.7)'
    };

    function handleHomeNavigation() {
        setRequestedForReload(prevState => !prevState)
        userStore.loadCurrentUser().then(() => setRequestedForReload(prevState => !prevState))
    }

    function handleResend() {
        if (timer <= 0) {
            userStore.resendVerificationEmail(userStore.currentUser!.email).then(isSuccess => {
                isSuccess && toast.success('Verification email sent - check your inbox')
            })
            setTimer(31);
        }
    }

    useEffect(() => {
        if (timer > 0) {
            const interval = setInterval(() => {
                setTimer(prevTimer => prevTimer - 1);
            }, 1000);

            return () => clearInterval(interval)
        }
    }, [timer]);

    function handleLogOut() {
        userStore.logOut()
        navigate('/logout')
    }

    return (
        <GradientContainer>
            {
                userStore.loading ?
                    <CircularProgress />
                    :
                    <>
                        <Typography variant={isMobile ? 'h4' : 'h2'} sx={headerStyles}>
                            Please verify your email
                        </Typography>
                        <Button
                            color={'success'}
                            onClick={() => handleHomeNavigation()}
                            variant="contained"
                            size="large"
                            sx={{marginBottom: 2, width: '300px'}}>
                            {
                                userStore.loading ?
                                    <CircularProgress />
                                    :
                                    'Go To Budget'
                            }
                        </Button>
                        {
                            userStore.currentUser &&
                                <>
                                    <Button
                                        color={'info'}
                                        disabled={timer > 0}
                                        onClick={() => handleResend()}
                                        variant="contained"
                                        size="large"
                                        sx={{marginBottom: 2, width: '300px'}}>
                                        {
                                            userStore.loading ?
                                                <CircularProgress />
                                                :
                                                'Resend verification email'
                                        }
                                    </Button>
                                    {timer > 0 && <p>Resend available in {timer} seconds</p>}
                                </>
                        }
                        {
                            userStore.currentUser &&
                            <>
                                <Button
                                    color={'error'}
                                    onClick={() => handleLogOut()}
                                    variant="contained"
                                    size="large"
                                    sx={{marginBottom: 2, width: '300px'}}>
                                    {
                                        userStore.loading ?
                                            <CircularProgress />
                                            :
                                            'Logout'
                                    }
                                </Button>
                            </>
                        }
                    </>
            }
        </GradientContainer>
    );
})