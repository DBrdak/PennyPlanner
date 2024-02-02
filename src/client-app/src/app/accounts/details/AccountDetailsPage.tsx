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
    useMediaQuery
} from "@mui/material";
import theme from "../../theme";
import {useEffect, useState} from "react";
import {Account} from "../../../models/accounts/account";
import TransactionsTable from "../components/transactionsTable/TransactionsTable";
import {Transaction} from "../../../models/transactions/transaction";
import formatDate from "../../../utils/dateFormatter";
import {AccountDetailsComponent} from "../components/AccountDetailsComponent";
import EditAccountComponent from "../components/EditAccountComponent";
import useTransactionEntities from "../../../utils/hooks/useTransactionEntities";
import useAccount from "../../../utils/hooks/useAccount";
import groupBy from "../../../utils/transactionsGroupBy";

interface  AccountDetailsPageProps {
    editMode: boolean
}

export default observer(function AccountDetailsPage({editMode}: AccountDetailsPageProps) {
    const [groupCriterion, setGroupCriterion] = useState('day')
    const [collapsedGroups, setCollapsedGroups] = useState<string[]>([])
    const [account, setAccount] = useState<Account | undefined>()
    const [groupedTransactions, setGroupedTransactions] = useState<Record<string, Transaction[]>>()
    const {accountStore} = useStore()
    const transactionEntities = useTransactionEntities(account)
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
    const { accountId } = useParams<{ accountId: string }>();
    const navigate = useNavigate()

    const resetCollapse = () => collapsedGroups.length > 0 && setCollapsedGroups([])

    useEffect(() => {
        const loadAccounts = async () => {
            await accountStore.loadAccounts();
        };

        const getAccount = (accountId: string) => accountStore.getAccount(accountId)

        if (accountStore.accounts.length < 1 && accountId) {
            loadAccounts().then(() => setAccount(accountStore.getAccount(accountId)));
        } else if(accountId && getAccount(accountId)) {
            setAccount(getAccount(accountId));
        } else {
            navigate('/not-found')
        }
        account && setGroupedTransactions(groupBy(account.transactions, groupCriterion))
    }, [account, accountId, accountStore, groupCriterion, groupedTransactions, navigate])

    const handleGroupChange = (groupKey: string) => {
        setGroupCriterion(groupKey)
        resetCollapse()
    }

    return (
        <AppOverlay>
            {!account || transactionEntities.length < 1 || !groupedTransactions || accountStore.loading ?
                <CircularProgress color={'secondary'} />
                :
                <Grid container sx={{
                    height:'100%',
                    padding: isMobile ? 3 : 4,
                    margin: 0,
                    backgroundColor: theme.palette.background.paper,
                    borderRadius: '20px',
                    overflow: isMobile ? 'auto' : 'hidden',
                }}>
                    {!editMode ?
                        <AccountDetailsComponent
                            account={account}
                            groupDropdownProps={{groupCriterion: groupCriterion, handleGroupChange: handleGroupChange}}
                        /> :
                        <EditAccountComponent
                            account={account}
                            groupDropdownProps={{groupCriterion: groupCriterion, handleGroupChange: handleGroupChange}}
                            setAccount={setAccount}
                        />
                    }
                    <Grid item xs={12} sx={{overflow: 'hidden', maxHeight: '50%'}}>
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