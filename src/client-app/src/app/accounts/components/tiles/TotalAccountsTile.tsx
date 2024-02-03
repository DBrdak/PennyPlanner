import {observer} from "mobx-react-lite";
import {useStore} from "../../../../stores/store";
import TilePaper from "../../../../components/tilesLayout/TilePaper";
import {Divider, Stack, Typography, useMediaQuery} from "@mui/material";
import theme from "../../../theme";
import {useNavigate} from "react-router-dom";
import {AccountBalanceDisplay} from "../details/AccountBalanceDisplay";

export default observer(function TotalAccountsTile() {
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
    const {accountStore} = useStore()
    const navigate = useNavigate()
    const transactions = accountStore.accounts.flatMap(a => a.transactions)
    const currency = transactions[0]?.transactionAmount.currency

    return (
        <TilePaper disabled colors={'achromatic'}>
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
                        Total
                    </Typography>
                </Stack>
                <Divider sx={{backgroundColor: theme.palette.background.paper}} />
                <AccountBalanceDisplay isMobile={isMobile} transactions={transactions} currency={currency} />
            </Stack>
        </TilePaper>
    );
})