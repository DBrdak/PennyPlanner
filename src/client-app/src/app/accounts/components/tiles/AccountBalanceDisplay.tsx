import {Divider, Stack, Typography} from "@mui/material";
import theme from "../../../theme";
import {Account} from "../../../../models/accounts/account";
import {calculateBalanceForCurrentMonth, calculateBalanceForToday} from "../../../../utils/calculators/balanceCalculator";
import {useNavigate} from "react-router-dom";
import {Transaction} from "../../../../models/transactions/transaction";
import {transaction} from "mobx";
import formatNumber from "../../../../utils/formatters/numberFormatter";

interface AccountBalanceDisplayProps {
    isMobile: boolean
    account?: Account
    transactions?: Transaction[]
    currency?: string
}

export function AccountBalanceDisplay({isMobile, account, transactions, currency}: AccountBalanceDisplayProps) {

    if(!account && (!transactions || !currency)){
        return null
    }

    const currentMonthBalance = account ?
        calculateBalanceForCurrentMonth(account.transactions) :
        calculateBalanceForCurrentMonth(transactions!)

    const todayBalance = account ?
        calculateBalanceForToday(account.transactions) :
        calculateBalanceForToday(transactions!)

    const generateBalanceTypography = (balance: number, color: string) => (
        <Typography
            sx={{
                fontSize: isMobile ? '1rem' : '1.75rem',
                userSelect: 'none',
                lineHeight: '1',
                color,
                textAlign: 'center',
                textOverflow: 'nowrap'
            }}
        >
            {balance > 0 ? `+${formatNumber(balance)}` : formatNumber(balance)} {account ? account.balance.currency : currency}
        </Typography>
    );

    const currentMonthBalanceTypography = generateBalanceTypography(
        currentMonthBalance,
        currentMonthBalance > 0
            ? theme.palette.success.light
            : currentMonthBalance < 0
                ? theme.palette.error.main
                : theme.palette.text.primary
    );

    const todayBalanceTypography = generateBalanceTypography(
        todayBalance,
        todayBalance > 0
            ? theme.palette.success.light
            : todayBalance < 0
                ? theme.palette.error.main
                : theme.palette.text.primary
    );

    const balance = account ?
        account.balance.amount :
            Number(transactions
                ?.map(t => t.transactionAmount)
                    .reduce((a, acc) => a + acc.amount, 0)
                        .toFixed(2))

    return (
        <Stack direction={'row'} justifyContent={'space-around'} sx={{width: '100%', height: '33%'}}>
            <Stack direction={'column'} justifyContent={'space-around'}>
                <Typography sx={{
                    fontSize: isMobile ? '1rem' : '1.75rem',
                    fontWeight: '700',
                    userSelect:'none',
                    lineHeight: '1',
                    color: theme.palette.text.primary,
                    textAlign: 'center',
                }}>
                    Balance
                </Typography>
                <Typography sx={{
                    fontSize: isMobile ? '1rem' : '1.75rem',
                    userSelect:'none',
                    lineHeight: '1',
                    color: theme.palette.text.primary,
                    textAlign: 'center',
                }}>
                    {formatNumber(balance)} {account ? account.balance.currency : currency}
                </Typography>
            </Stack>
            <Divider orientation={'vertical'} variant={'middle'} sx={{backgroundColor: theme.palette.background.paper}} />
            <Stack direction={'column'} justifyContent={'space-around'}>
                <Typography sx={{
                    fontSize: isMobile ? '1rem' : '1.75rem',
                    fontWeight: '700',
                    userSelect:'none',
                    lineHeight: '1',
                    color: theme.palette.text.primary,
                    textAlign: 'center',
                    textOverflow: 'nowrap'
                }}>
                    {new Date().toLocaleString('en-US', { month: 'long' })}
                </Typography>
                {currentMonthBalanceTypography}
            </Stack>
            <Divider orientation={'vertical'} variant={'middle'} sx={{backgroundColor: theme.palette.background.paper}} />
            <Stack direction={'column'} justifyContent={'space-around'}>
                <Typography sx={{
                    fontSize: isMobile ? '1rem' : '1.75rem',
                    fontWeight: '700',
                    userSelect:'none',
                    lineHeight: '1',
                    color: theme.palette.text.primary,
                    textAlign: 'center',
                    textOverflow: 'nowrap'
                }}>
                    Today
                </Typography>
                {todayBalanceTypography}
            </Stack>
        </Stack>
    );
}