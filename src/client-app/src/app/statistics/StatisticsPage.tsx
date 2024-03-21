import AppOverlay from "../../components/appOverlay/AppOverlay";
import {observer} from "mobx-react-lite";
import useTitle from "../../utils/hooks/useTitle";
import {Grid, Typography, useMediaQuery} from "@mui/material";
import theme from "../theme";
import useAuthProvider from "../../utils/hooks/useAuthProvider";

export default observer(function StatisticsPage() {
    useAuthProvider()
    const isMobile = useMediaQuery(theme.breakpoints.down('md'))
    useTitle('Statistics')

    return (
        <AppOverlay>
            <Grid container sx={{
                padding: 2,
                margin: 0,
                backgroundColor: theme.palette.background.paper,
                borderRadius: '20px',
                overflowY:'auto',
                overflowX:'hidden',
                maxWidth: '1920px',
                flexDirection: 'column',
                textAlign: 'center',
                display: 'flex', alignItems: 'center', justifyContent: 'center',
                gap: theme.spacing(4)
            }}>
                <Typography variant={isMobile ? 'h3' : 'h1'} fontWeight={'bold'}>
                    Hi there! 👋
                </Typography>
                <Typography variant={isMobile ? 'h5' : 'h4'} color={'secondary'}>
                    This brilliant feature will be available soon!
                </Typography>
                <Typography variant={isMobile ? 'h6' : 'h4'}>
                    Until then, feel free to check out our other features
                </Typography>
            </Grid>
        </AppOverlay>
    );
})