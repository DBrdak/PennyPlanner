import TilePaper from "../../../../components/tilesLayout/TilePaper";
import {Button, Grid, Stack, Typography} from "@mui/material";
import {TilesLayout} from "../../../../components/tilesLayout/TilesLayout";
import Tile from "../../../../models/layout/tile";
import theme from "../../../theme";

export function AddTransactionTile() {
    //TODO make it as comfortable as possible - it will be used for most time


    return (
        <TilePaper disabled sx={{padding: 0.5}}>
            <Grid container height={'100%'}>
                <Grid item xs={12} lg={6} sx={{display: 'flex', justifyContent: 'center', alignItems: 'center', height: '50%'}}>
                    <Typography fontSize={'2vw'} textAlign={'center'}>
                        Add Transaction
                    </Typography>
                </Grid>

                <Grid item xs={12} lg={6} sx={{display: 'flex', justifyContent: 'center', alignItems: 'center', height: '50%'}}>
                    <Button variant={'outlined'} color={'info'} sx={{
                        width: '100%',
                        height: '100%',
                        borderRadius: 0,
                        fontSize: '2vw',
                        border: 'none'
                    }}>
                        Internal
                    </Button>
                </Grid>
                <Grid item xs={12} lg={6} sx={{display: 'flex', justifyContent: 'center', alignItems: 'center', height: '50%'}}>
                    <Button variant={'outlined'} color={'success'} sx={{
                        width: '100%',
                        height: '100%',
                        borderRadius: 0,
                        fontSize: '2vw',
                        border: 'none'
                    }}>
                        Income
                    </Button>
                </Grid>
                <Grid item xs={12} lg={6} sx={{display: 'flex', justifyContent: 'center', alignItems: 'center', height: '50%'}}>
                    <Button variant={'outlined'} color={'error'} sx={{
                        width: '100%',
                        height: '100%',
                        borderRadius: 0,
                        fontSize: '2vw',
                        border: 'none'
                    }}>
                        Outcome
                    </Button>
                </Grid>

            </Grid>
        </TilePaper>
    );
}