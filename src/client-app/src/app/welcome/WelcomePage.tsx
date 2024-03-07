import {Button, ButtonGroup, CircularProgress, Typography, useMediaQuery, useTheme} from "@mui/material";
import {useNavigate} from "react-router-dom";
import '../../styles/index.css'
import GradientContainer from "./GradientContainer";
import useTitle from "../../utils/hooks/useTitle";
import {observer} from "mobx-react-lite";
import {useEffect} from "react";
import {useStore} from "../../stores/store";

const WelcomePage: React.FC = () => {
    const theme = useTheme();
    const isMobile = useMediaQuery(theme.breakpoints.down('sm'));
    const navigate = useNavigate()
    const {userStore} = useStore()
    useTitle()

    useEffect(() => {
        userStore.token && userStore.loadCurrentUser()
    }, [userStore])

    const headerStyles: React.CSSProperties = {
        color: theme.palette.common.white,
        marginBottom: theme.spacing(3),
        userSelect: 'none',
        textShadow: '2px 2px 6px rgba(0,0,0, 0.7)'
    };

    return (
        <GradientContainer>
            {
                userStore.loading ?
                    <CircularProgress />
                    :
                    <>
                        <Typography variant={isMobile ? 'h4' : 'h2'} sx={headerStyles}>
                            Welcome to Penny Planner
                        </Typography>
                        <Typography variant="h6" color="textSecondary" align="center" sx={{
                            marginBottom: theme.spacing(3),
                            userSelect: 'none',
                            textShadow: '0px 0px 6px rgba(0,0,0, 0.6)'
                        }}>
                            Your budget management made easy.
                        </Typography>
                        {userStore.currentUser ? (
                            <Button
                                color={'success'}
                                onClick={() => navigate('/home')}
                                variant="contained"
                                size="large">
                                Go to Budget
                            </Button>
                        ) : (
                            <ButtonGroup>
                                <Button
                                    onClick={() => navigate('/login')}
                                    variant="contained"
                                    size="large"
                                    sx={{
                                        color: theme.palette.text.primary,
                                        backgroundColor: theme.palette.primary.dark,
                                        '&:hover': {backgroundColor: theme.palette.background.paper}
                                    }}>
                                    Sign In
                                </Button>
                                <Button
                                    onClick={() => navigate('/register')}
                                    variant="contained"
                                    size="large"
                                    sx={{
                                        color: 'black',
                                        backgroundColor: theme.palette.secondary.light,
                                        '&:hover': {backgroundColor: theme.palette.text.secondary}
                                    }}>
                                    Sign Up
                                </Button>
                            </ButtonGroup>
                        )}
                    </>
            }
        </GradientContainer>
    );
};

export default observer(WelcomePage)