import AppOverlay from "../../components/appOverlay/AppOverlay";
import {observer} from "mobx-react-lite";
import useTitle from "../../utils/hooks/useTitle";
import {Grid, Typography} from "@mui/material";
import theme from "../theme";

export default observer(function StatisticsPage() {

    useTitle('Statistics')

    return (
        <AppOverlay>
            <Grid container sx={{
                height:'100%',
                padding: 2,
                margin: 0,
                backgroundColor: theme.palette.background.paper,
                borderRadius: '20px',
                overflow:'auto',
                maxWidth: '1920px',
                flexDirection: 'column',
                textAlign: 'center',
                display: 'flex', alignItems: 'center', justifyContent: 'center',
                gap: theme.spacing(4)
            }}>
                <Typography variant={'h1'} fontWeight={'bold'}>
                    Hi there! 👋
                </Typography>
                <Typography variant={'h3'} color={'secondary'}>
                    This brilliant feature will be available soon!
                </Typography>
                <Typography variant={'h4'}>
                    Until then, feel free to check out our other features
                </Typography>
            </Grid>
        </AppOverlay>
    );
})