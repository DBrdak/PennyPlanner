import {observer} from "mobx-react-lite";
import {useStore} from "../../../../stores/store";
import TilePaper from "../../../../components/tilesLayout/TilePaper";
import {Divider, Grid, Typography, useMediaQuery} from "@mui/material";
import theme from "../../../theme";
import {useNavigate} from "react-router-dom";
import {AccountBalanceDisplay} from "./AccountBalanceDisplay";

export default observer(function TotalAccountsTile() {
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
    const {accountStore} = useStore()
    const navigate = useNavigate()
    const transactions = accountStore.accounts.flatMap(a => a.transactions)
    const currency = transactions[0]?.transactionAmount.currency

    return (
        <TilePaper disabled disableBorder>
            <Grid item xs={12} sx={{height: '100%'}}>
                <Grid item xs={12} sx={{
                    height: '40%',
                    display: 'flex', justifyContent: 'center', alignItems: 'center',
                    flexDirection: 'column', gap: theme.spacing(2)
                }}>
                    <Typography sx={{
                        fontSize: isMobile ? '2rem' : '2.5rem',
                        fontWeight: '700',
                        userSelect:'none',
                        lineHeight: '1',
                        color: theme.palette.text.primary,
                        textAlign: 'center',
                    }}>
                        Total
                    </Typography>
                </Grid>
                <Divider variant={'middle'} />
                <Grid item xs={12} sx={{
                    height: '55%',
                    display: 'flex', justifyContent: 'center', alignItems: 'center',
                }}>
                    <AccountBalanceDisplay isMobile={isMobile} transactions={transactions} currency={currency} />
                </Grid>
            </Grid>
        </TilePaper>
    );
})