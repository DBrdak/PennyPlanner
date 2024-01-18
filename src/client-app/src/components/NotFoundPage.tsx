import {useEffect, useState} from "react";
import theme from "../app/theme";
import {Box, CircularProgress, LinearProgress, Typography, useMediaQuery} from "@mui/material";
import {useNavigate} from "react-router-dom";

interface NotFoundPageProps {
    text?: string;
}

const NotFoundPage: React.FC<NotFoundPageProps> = ({ text }) => {
    const [progress, setProgress] = useState(0);
    const navigate = useNavigate();

    useEffect(() => {
        const timer = setInterval(() => {
            setProgress((oldProgress) => {
                if (oldProgress === 100) {
                    clearInterval(timer)
                    navigate('/')
                    return 100;
                }
                return Math.min(oldProgress + 1, 100);
            });
        }, 25);

        return () => {
            clearInterval(timer);
        };
    }, []);

    return (
        <Box
            display="flex"
            flexDirection="column"
            alignItems="center"
            justifyContent="center"
            height="100vh"
        >
            <Typography variant="h6" marginBottom={2}>
                {text}
            </Typography>
            <LinearProgress variant='determinate' color='primary' value={progress} style={{ width: '500px' }} />
        </Box>
    );
};

export default NotFoundPage;