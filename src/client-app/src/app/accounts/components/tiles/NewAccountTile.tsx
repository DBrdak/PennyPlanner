import TilePaper from "../../../../components/tilesLayout/TilePaper";
import {useNavigate} from "react-router-dom";
import {Divider, Stack, Typography, useMediaQuery} from "@mui/material";
import theme from "../../../theme";
import {AddCardTwoTone} from "@mui/icons-material";
import {AccountBalanceDisplay} from "../details/AccountBalanceDisplay";

export function NewAccountTile() {
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
    const navigate = useNavigate()

    return (
        <TilePaper onClick={() => navigate('/accounts/new')} colors={'cyan'}>
            <Stack spacing={isMobile ? 2 : 5}
                   sx={{
                       p: '1vw',
                       color: 'white',
                       filter: 'none',
                       textShadow: '2px 2px 8px rgba(0, 0, 0, 0.8)',
                       borderRadius: '25px',
                       width: '85%',
                       height: '90%',
                       zIndex: 100,
                       textOverflow: 'wrap', display: 'flex',
                       justifyContent: 'center',
                   }}
            >
                <Stack direction={'column'} spacing={2}>
                    <Typography sx={{
                        fontSize: isMobile ? '2rem' : '2.5rem',
                        fontWeight: '700',
                        userSelect:'none',
                        lineHeight: '1',
                        color: theme.palette.text.primary,
                        textAlign: 'center',
                    }}>
                        New Account
                    </Typography>
                    <Typography sx={{
                        userSelect:'none',
                        lineHeight: '1',
                        color: theme.palette.text.primary,
                        textAlign: 'center',
                    }}>
                        <AddCardTwoTone sx={{fontSize: '4rem'}} />
                    </Typography>
                </Stack>
            </Stack>
        </TilePaper>
    );
}