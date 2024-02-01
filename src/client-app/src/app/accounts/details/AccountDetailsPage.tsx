import {useNavigate, useParams} from "react-router-dom";
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
    const navigate = useNavigate()

    useEffect(() => {
        const loadAccounts = async () => {
            await accountStore.loadAccounts();
        };

        if (accountStore.accounts.length < 1 && accountId) {
            loadAccounts().then(() => setAccount(accountStore.getAccount(accountId)));
        } else if(accountId) {
            setAccount(accountStore.getAccount(accountId));
        } else {
            navigate('/not-found')
        }
    }, [accountStore, accountId]);

    return account;
};

const useTransactionEntities = (account: Account | undefined) => {
    const {transactionEntityStore} = useStore()

    useEffect(() => {
        const loadTransactionEntities = async () => await transactionEntityStore.loadTransactionEntities()

        if(account) {
            const requiredTransactionEntities = Array.from(new Set([
                ...account.transactions.flatMap(t => t.senderId),
                ...account.transactions.flatMap(t => t.recipientId)
            ]))

            if(transactionEntityStore.transactionEntities.length < requiredTransactionEntities.length) {
                loadTransactionEntities()
            }
        }

    }, [account, transactionEntityStore])
}

export default observer(function AccountDetailsPage() {
    const [groupBy, setGroupBy] = useState('day')
    const [collapsedGroups, setCollapsedGroups] = useState<string[]>([])
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
    const account = useAccount();

    useTransactionEntities(account)

    const resetCollapse = () => collapsedGroups.length > 0 && setCollapsedGroups([])

    const handleGroupChange = (groupKey: string) => {
        setGroupBy(groupKey)
        resetCollapse()
    }

    return (
        <AppOverlay>
            {!account ? <CircularProgress color={'secondary'} /> :
                <Grid container sx={{
                    height:'100%',
                    padding: isMobile ? 3 : 4,
                    margin: 0,
                    backgroundColor: theme.palette.background.paper,
                    borderRadius: '20px',
                    overflow:'hidden'
                }}>
                    <Grid item xs={12} sx={{textAlign: 'center'}}>
                        <Typography variant={'h4'} sx={{userSelect: 'none'}}>
                            {account.name}
                        </Typography>
                        <FormControl>
                            <InputLabel>Group By</InputLabel>
                            <Select fullWidth value={groupBy} onChange={(e) => handleGroupChange(e.target.value)}>
                                <MenuItem key={1} value={'day'}>Day</MenuItem>
                                <MenuItem key={2} value={'month'}>Month</MenuItem>
                                <MenuItem key={3} value={'year'}>Year</MenuItem>
                                <MenuItem key={4} value={'entity'}>Entity</MenuItem>
                                <MenuItem key={5} value={'category'}>Category</MenuItem>
                            </Select>
                        </FormControl>
                        <TransactionsTable
                            transactions={account.transactions}
                            groupCriterion={groupBy}
                            collapsedGroups={collapsedGroups}
                            setCollapsedGroups={setCollapsedGroups}
                        />
                    </Grid>
                </Grid>
            }
        </AppOverlay>
    );
})