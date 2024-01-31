import {Button, ButtonGroup, Container, Typography, useMediaQuery, useTheme} from "@mui/material";
import {useNavigate} from "react-router-dom";
import '../../styles/index.css'
import GradientContainer from "./GradientContainer";

const WelcomePage: React.FC = () => {
    const theme = useTheme();
    const isMobile = useMediaQuery(theme.breakpoints.down('sm'));
    const navigate = useNavigate()

    const headerStyles: React.CSSProperties = {
        color: theme.palette.common.white,
        marginBottom: theme.spacing(3),
        userSelect: 'none',
        textShadow: '2px 2px 6px rgba(0,0,0, 0.7)'
    };

    return (
        <GradientContainer>
            <Typography variant={isMobile ? 'h4' : 'h2'} sx={headerStyles}>
                Welcome to Budgetify
            </Typography>
            <Typography variant="h6" color="textSecondary" align="center" sx={{
                marginBottom: theme.spacing(3),
                userSelect: 'none',
                textShadow: '0px 0px 6px rgba(0,0,0, 0.6)'
            }}>
                Your budget management made easy.
            </Typography>
            {2 % 3 === 0 ? (
                <Button
                    onClick={() => navigate('/home')}
                    variant="contained"
                    size="large"
                    sx={{
                        backgroundColor: theme.palette.primary.main
                }}>
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
        </GradientContainer>
    );
};

export default WelcomePage;