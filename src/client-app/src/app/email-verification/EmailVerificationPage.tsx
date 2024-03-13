import {observer} from "mobx-react-lite";
import {useStore} from "../../stores/store";
import {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";
import GradientContainer from "../welcome/GradientContainer";
import {Button, ButtonGroup, CircularProgress, Typography, useMediaQuery} from "@mui/material";
import theme from "../theme";
import {ArrowRight, InboxTwoTone} from "@mui/icons-material";
import {toast} from "react-toastify";

export default observer(function EmailVerificationPage() {
    const {userStore} = useStore()
    const navigate = useNavigate()
    const isMobile = useMediaQuery(theme.breakpoints.down('sm'));
    const [requestedForReload, setRequestedForReload] = useState(false)

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
                        <Button variant={'contained'} onClick={() => handleHomeNavigation()}>
                            {
                                userStore.loading ?
                                    <CircularProgress />
                                    :
                                    <Typography variant={'h6'}>
                                        Go To Budget
                                    </Typography>
                            }
                        </Button>
                    </>
            }
        </GradientContainer>
    );
})