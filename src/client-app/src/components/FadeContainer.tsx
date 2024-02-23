import {keyframes, styled, useMediaQuery} from "@mui/material";
import MuiBox from "@mui/material/Box";
import theme from "../app/theme";

const fadeIn = keyframes`
            0% { opacity: 0; }
            100% { opacity: 1; }
        `

export const FadeContainer = styled(MuiBox)(
    (theme) => ({
        animation: `${fadeIn} 2s`,
        minWidth: '99svw',
        minHeight: '100svh',
        overflowX: 'hidden'
    })
);