import {Grid, useMediaQuery} from "@mui/material";
import theme from "../../app/theme";
import '../../styles/index.css'
import Tile from "../../models/layout/tile";

interface TilesLayoutProps {
    tiles: Tile[];
}

export function TilesLayout({ tiles }: TilesLayoutProps) {
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))

    return (
        <Grid container sx={{
            height:'100%',
            padding: isMobile ? 1 : 2,
            margin: 0,
            backgroundColor: theme.palette.background.paper,
            borderRadius: '20px',
            overflow:'auto',
            width: '100%',
            maxWidth: '1920px',
        }}>

            {tiles.map((tile, index) => (
                <Grid key={index} item xs={12} md={tile.cols} height={tile.height} padding={isMobile ? 0.5 : 1}>
                    {tile.content}
                </Grid>
            ))}
        </Grid>
    );
};