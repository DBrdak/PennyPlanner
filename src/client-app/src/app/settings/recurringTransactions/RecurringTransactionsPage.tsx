import AppOverlay from "../../../components/appOverlay/AppOverlay";
import {Grid} from "@mui/material";
import theme from "../../theme";

export default function RecurringTransactionsPage() {
    return (
        <AppOverlay>
            <Grid container sx={{
                height:'100%',
                padding: 2,
                margin: 0,
                backgroundColor: theme.palette.background.paper,
                borderRadius: '20px',
                overflow:'auto'
            }}>

            </Grid>
        </AppOverlay>
    );
}