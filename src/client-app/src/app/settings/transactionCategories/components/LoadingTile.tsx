import {CircularProgress, Grid} from "@mui/material";

export function LoadingTile() {
    return (
        <Grid item xs={12} sx={{
            minHeight: '200px',
            height: '33%',
            marginBottom: 3,
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center'
        }}>
            <CircularProgress />
        </Grid>
    );
}