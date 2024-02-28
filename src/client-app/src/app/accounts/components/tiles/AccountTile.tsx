import {Divider, Grid, Stack, Typography, useMediaQuery} from "@mui/material";
import TilePaper from "../../../../components/tilesLayout/TilePaper";
import {useNavigate} from "react-router-dom";
import theme from "../../../theme";
import {Account} from "../../../../models/accounts/account";
import {AccountBalanceDisplay} from "./AccountBalanceDisplay";

export interface AccountTileProps {
    account: Account
}

export function AccountTile({account}: AccountTileProps) {
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
    const navigate = useNavigate()

    return (
        <TilePaper onClick={() => navigate(`/accounts/${account.accountId}`)}>
            <Grid item xs={12} sx={{height: '100%'}}>
                <Grid item xs={12} sx={{
                    height: '40%',
                    display: 'flex', justifyContent: 'center', alignItems: 'center',
                    flexDirection: 'column', gap: theme.spacing(2)
                }}>
                    <Typography sx={{
                        fontSize:  '2.5rem',
                        fontWeight: '700',
                        userSelect:'none',
                        lineHeight: '1',
                        color: theme.palette.text.primary,
                        textAlign: 'center',
                    }}>
                        {account.name}
                    </Typography>
                    <Typography variant={'h6'} sx={{
                        userSelect:'none',
                        lineHeight: '1',
                        color: theme.palette.text.primary,
                        textAlign: 'center',
                    }}>
                        {account.accountType}
                    </Typography>
                </Grid>
                <Divider variant={'middle'} />
                <Grid item xs={12} sx={{
                    height: '55%',
                    display: 'flex', justifyContent: 'center', alignItems: 'center'
                }}>
                    <AccountBalanceDisplay isMobile={isMobile} account={account}/>
                </Grid>
            </Grid>
            {
                /*
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
                        fontSize:  '2.5rem',
                        fontWeight: '700',
                        userSelect:'none',
                        lineHeight: '1',
                        color: theme.palette.text.primary,
                        textAlign: 'center',
                    }}>
                        {account.name}
                    </Typography>
                    <Typography variant={'h6'} sx={{
                        userSelect:'none',
                        lineHeight: '1',
                        color: theme.palette.text.primary,
                        textAlign: 'center',
                    }}>
                        {account.accountType}
                    </Typography>
                </Stack>
                <Divider sx={{backgroundColor: theme.palette.background.paper}} />
                <AccountBalanceDisplay isMobile={isMobile} account={account}/>
            </Stack>
                 */
            }
        </TilePaper>
    );
}