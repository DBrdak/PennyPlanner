import {useNavigate, useParams} from "react-router-dom";
import AppOverlay from "../../../components/appOverlay/AppOverlay";
import {useStore} from "../../../stores/store";
import {observer} from "mobx-react-lite";
import {
    Button,
    CircularProgress, Divider,
    FormControl,
    Grid,
    InputLabel,
    MenuItem,
    Select, Stack,
    Typography,
    useMediaQuery
} from "@mui/material";
import theme from "../../theme";
import {useEffect, useState} from "react";
import {Account} from "../../../models/accounts/account";
import TransactionsTable from "../components/transactionsTable/TransactionsTable";
import {Transaction} from "../../../models/transactions/transaction";
import formatDate from "../../../utils/dateFormatter";

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

    return transactionEntityStore.transactionEntities
}

export default observer(function AccountDetailsPage() {
    const [editMode, setEditMode] = useState(false)
    const [groupCriterion, setGroupCriterion] = useState('day')
    const [collapsedGroups, setCollapsedGroups] = useState<string[]>([])
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
    const account = useAccount();
    const transactionEntities = useTransactionEntities(account)
    const navigate = useNavigate()

    const resetCollapse = () => collapsedGroups.length > 0 && setCollapsedGroups([])

    const handleGroupChange = (groupKey: string) => {
        setGroupCriterion(groupKey)
        resetCollapse()
    }

    const groupBy = (transactions: Transaction[], criterion: string): Record<string, Transaction[]> => {
        const groupedTransactions: Record<string, Transaction[]> = {};

        transactions.forEach((transaction) => {
            let key

            switch (criterion) {
                case 'day':
                    key = formatDate(transaction.transactionDateUtc).slice(0, 10)
                    break
                case 'month':
                    key = formatDate(transaction.transactionDateUtc).slice(3, 10)
                    break
                case 'year':
                    key = formatDate(transaction.transactionDateUtc).slice(6, 10)
                    break
                case 'entity':
                    key = transaction['recipientId' || 'senderId' || 'fromAccountId' || 'toAccountId' as keyof Transaction] as string || 'Private'
                    break
                case 'category':
                    key = transaction[criterion as keyof Transaction] as string || 'Unknown'
                    break
            }

            if (key && !groupedTransactions[key]) {
                groupedTransactions[key] = [];
            }

            key && groupedTransactions[key].push(transaction);
        });

        return groupedTransactions;
    };


    const groupedTransactions = account && groupBy(account.transactions, groupCriterion);

    return (
        <AppOverlay>
            {!account || transactionEntities.length < 1 || !groupedTransactions ? <CircularProgress color={'secondary'} /> :
                <Grid container sx={{
                    height:'100%',
                    padding: isMobile ? 3 : 4,
                    margin: 0,
                    backgroundColor: theme.palette.background.paper,
                    borderRadius: '20px',
                    overflow:'hidden'
                }}>
                    {!editMode &&
                        <>
                            <Grid item xs={12} sx={{display: 'flex', alignItems:'center', justifyContent: 'center'}}>
                                <Stack sx={{justifyContent: 'center', alignItems: 'center'}}>
                                    <Typography variant={"subtitle1"} sx={{ userSelect: "none" }}>
                                        Account Name
                                    </Typography>
                                    <Typography variant={"h4"} sx={{ userSelect: "none" }}>
                                        {account.name}
                                    </Typography>
                                </Stack>
                            </Grid>
                            <Grid item xs={12} sx={{display: 'flex', alignItems:'center', justifyContent: 'center'}}>
                                <Stack sx={{justifyContent: 'center', alignItems: 'center'}}>
                                    <Typography variant={"subtitle1"} sx={{ userSelect: "none" }}>
                                        Account Type
                                    </Typography>
                                    <Typography variant={"h4"} sx={{ userSelect: "none" }}>
                                        {account.accountType}
                                    </Typography>
                                </Stack>
                            </Grid>
                            <Grid item xs={12} sx={{display: 'flex', alignItems:'center', justifyContent: 'center'}}>
                                <Stack sx={{justifyContent: 'center', alignItems: 'center'}}>
                                    <Typography variant={"subtitle1"} sx={{ userSelect: "none" }}>
                                        Account Balance
                                    </Typography>
                                    <Typography variant={"h4"} sx={{ userSelect: "none" }}>
                                        {account.balance.amount.toFixed(2)} {account.balance.currency}
                                    </Typography>
                                </Stack>
                            </Grid>
                            <Grid item xs={12} md={6} marginBottom={3} sx={{display: 'flex', alignItems:'center', justifyContent: 'center'}}>
                                <Button
                                    variant="contained"
                                    color="primary"
                                    sx={{ width: '60%'}}
                                    onClick={() => setEditMode(true)}
                                >
                                    Edit Account
                                </Button>
                            </Grid>
                        </>
                    }
                    <Grid item xs={12} md={6} marginBottom={3} sx={{display: 'flex', alignItems:'center', justifyContent: 'center'}}>
                        <FormControl sx={{width: '60%'}}>
                            <InputLabel>Group By</InputLabel>
                            <Select fullWidth value={groupCriterion}
                                    onChange={(e) => handleGroupChange(e.target.value)}>
                                <MenuItem key={1} value={'day'}>Day</MenuItem>
                                <MenuItem key={2} value={'month'}>Month</MenuItem>
                                <MenuItem key={3} value={'year'}>Year</MenuItem>
                                <MenuItem key={4} value={'entity'}>Entity</MenuItem>
                                <MenuItem key={5} value={'category'}>Category</MenuItem>
                            </Select>
                        </FormControl>
                    </Grid>
                    <Grid item xs={12} sx={{overflow: 'hidden', height: '50%'}}>
                        <TransactionsTable
                            groupCriterion={groupCriterion}
                            collapsedGroups={collapsedGroups}
                            setCollapsedGroups={setCollapsedGroups}
                            groupedTransactions={groupedTransactions}
                        />
                    </Grid>
                </Grid>
            }
        </AppOverlay>
    );
})