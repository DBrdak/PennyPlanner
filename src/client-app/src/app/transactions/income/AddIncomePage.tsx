import AppOverlay from "../../../components/appOverlay/AppOverlay";
import theme from "../../theme";
import {Grid} from "@mui/material"

export function AddIncomePage() {

    return (
        <AppOverlay>
            <Grid container sx={{
                height:'100%',
                padding: 3,
                margin: 0,
                backgroundColor: theme.palette.background.paper,
                borderRadius: '20px',
                overflow: 'auto',
            }}>
            </Grid>
        </AppOverlay>
    );
}