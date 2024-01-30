import {Divider, Stack, Typography} from "@mui/material";
import theme from "../../theme";
import {Account} from "../../../models/accounts/account";
import {calculateBalanceForCurrentMonth, calculateBalanceForToday} from "../../../utils/calculators/balanceCalculator";

interface AccountBalanceDisplayProps {
    isMobile: boolean
    account: Account
}

export function AccountBalanceDisplay({isMobile, account}: AccountBalanceDisplayProps) {
    const currentMonthBalance = calculateBalanceForCurrentMonth(account)
    const todayBalance = calculateBalanceForToday(account)

    const generateBalanceTypography = (balance: number, color: string) => (
        <Typography
            sx={{
                fontSize: isMobile ? '1rem' : '1.75rem',
                userSelect: 'none',
                lineHeight: '1',
                color,
                textAlign: 'center',
            }}
        >
            {balance > 0 ? `+${balance}` : balance} {account.balance.currency}
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
                    {account.balance.amount} {account.balance.currency}
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
                }}>
                    Today
                </Typography>
                {todayBalanceTypography}
            </Stack>
        </Stack>
    );
}