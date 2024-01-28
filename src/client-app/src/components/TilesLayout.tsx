import {Grid, Paper} from "@mui/material";

interface TilesLayoutProps {
    tiles: Tile[];
}

export function TilesLayout({ tiles }: TilesLayoutProps) {
    const rowsNumber = Math.ceil(tiles.reduce((acc, tile) => acc + tile.cols, 0) / 12)

    return (
        <Grid container spacing={2} sx={{height:'100%'}}>
            {tiles.map((tile, index) => (
                <Grid key={index} item xs={12} sm={tile.cols} style={{height: `calc((${tile.rows} / 12) * 100)%`}}>
                    <Paper style={{ padding: 16, display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100%' }}>
                        {tile.content}
                    </Paper>
                </Grid>
            ))}
        </Grid>
    );
};