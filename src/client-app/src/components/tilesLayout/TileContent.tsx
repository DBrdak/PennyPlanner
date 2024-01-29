import {Box, Paper, Typography, useMediaQuery} from "@mui/material";
import TilePaper from "./TilePaper";
import theme from "../../app/theme";
import {useNavigate} from "react-router-dom";

export interface TileContentProps {
    text: string
    img: string
    navigateToPath: string
}

export function TileContent({text, img, navigateToPath}: TileContentProps) {
    const isMobile = useMediaQuery(theme.breakpoints.down('xl'))
    const navigate = useNavigate()

    return (
        <TilePaper img={img} onClick={() => navigate(navigateToPath)}>
            <Box
                sx={{
                    position: 'absolute',
                    bottom: isMobile ? 0 : '2vh',
                    right: 0,
                    p: '1vw',
                    color: 'white',
                    filter: 'none',
                    textShadow: '2px 2px 12px rgba(0, 0, 0, 1)',
                    borderRadius: isMobile ? '25px 0px 0px 0px' : '25px 0px 0px 25px',
                    backgroundColor: theme.palette.background.paper,
                    marginLeft: '3vw',
                    zIndex: 100,
                }}
            >
                <Typography sx={{
                    fontSize: isMobile ? '2vw' : '2.5vw',
                    fontWeight: '700',
                    userSelect:'none',
                    letterSpacing: '0.7vw',
                    lineHeight: '1'
                }}>
                    {text}
                </Typography>
            </Box>
        </TilePaper>
    );
}