import {Grid, Paper, Stack} from "@mui/material";

interface TilesLayoutProps {
    tiles: Tile[];
}

export function TilesLayout({ tiles }: TilesLayoutProps) {
    const rowsNumber = Math.ceil(tiles.reduce((acc, tile) => acc + tile.cols, 0) / 12)

    return (
        <Grid container spacing={2} sx={{height:'100%'}}>
            {tiles.map((tile, index) => (
                <Grid key={index} item xs={12} sm={tile.cols}>
                    {tile.content.map((content, contentIndex) => (
                        <Stack direction={'column'} gap={2} sx={{height: `calc(100% / ${tile.content.length})`}}>
                            <Paper style={{ padding: 16, display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100%' }}>
                                {content}
                            </Paper>
                        </Stack>
                    ))}
                </Grid>
            ))}
        </Grid>
    );
};