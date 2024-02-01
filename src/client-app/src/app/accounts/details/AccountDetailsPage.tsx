import {useParams} from "react-router-dom";
import AppOverlay from "../../../components/appOverlay/AppOverlay";
import {useStore} from "../../../stores/store";
import {observer} from "mobx-react-lite";
import {
    CircularProgress,
    FormControl,
    Grid,
    InputLabel,
    MenuItem,
    Select,
    Typography,
    useMediaQuery
} from "@mui/material";
import theme from "../../theme";
import {useEffect, useState} from "react";
import {Account} from "../../../models/accounts/account";
import {TransactionsTable} from "../components/transactionsTable/TransactionsTable";

const useAccount = () => {
    const { accountStore } = useStore();
    const { accountId } = useParams<{ accountId: string }>();
    const [account, setAccount] = useState<Account | undefined>();

    useEffect(() => {
        const loadAccounts = async () => {
            await accountStore.loadAccounts();
        };

        const findAccount = () => accountStore.accounts.find(a => a.accountId === accountId);

        if (accountStore.accounts.length < 1) {
            loadAccounts().then(() => setAccount(findAccount()));
        } else {
            setAccount(findAccount());
        }
    }, [accountStore, accountId]);

    return account;
};

export default observer(function AccountDetailsPage() {
    const [groupBy, setGroupBy] = useState('day')
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
    const account = useAccount();

    return (
        <AppOverlay>
            {!account ? <CircularProgress color={'secondary'} /> :
                <Grid container sx={{
                    height:'100%',
                    padding: isMobile ? 3 : 4,
                    margin: 0,
                    backgroundColor: theme.palette.background.paper,
                    borderRadius: '20px',
                    overflow:'auto'
                }}>
                    <Grid item xs={12} sx={{textAlign: 'center'}}>
                        <Typography variant={'h4'} sx={{userSelect: 'none'}}>
                            {account.name}
                        </Typography>
                        <FormControl>
                            <InputLabel>Group By</InputLabel>
                            <Select fullWidth value={groupBy} onChange={(e) => setGroupBy(e.target.value)}>
                                <MenuItem key={1} value={'day'}>Day</MenuItem>
                                <MenuItem key={2} value={'month'}>Month</MenuItem>
                                <MenuItem key={3} value={'year'}>Year</MenuItem>
                                <MenuItem key={4} value={'sender/recipient'}>Sender/Recipient</MenuItem>
                                <MenuItem key={5} value={'category'}>Category</MenuItem>
                            </Select>
                        </FormControl>
                        <TransactionsTable transactions={account.transactions} groupCriterion={groupBy} />
                    </Grid>
                </Grid>
            }
        </AppOverlay>
    );
})