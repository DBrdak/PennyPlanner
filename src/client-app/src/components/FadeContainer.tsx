import {keyframes, styled} from "@mui/material";
import MuiBox from "@mui/material/Box";

const fadeIn = keyframes`
            0% { opacity: 0; }
            100% { opacity: 1; }
        `

export const FadeContainer = styled(MuiBox)(
    (theme) => ({
        animation: `${fadeIn} 2s`,
        minWidth: '100vw',
        height: '100vh',
        overflowY: 'hidden',
    })
);