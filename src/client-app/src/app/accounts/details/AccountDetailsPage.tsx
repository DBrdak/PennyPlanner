import AppOverlay from "../../../components/appOverlay/AppOverlay";
import {useStore} from "../../../stores/store";
import {observer} from "mobx-react-lite";
import {
    CircularProgress,
    Grid,
    useMediaQuery
} from "@mui/material";
import theme from "../../theme";
import {useEffect, useState} from "react";
import TransactionsTable from "../components/transactionsTable/TransactionsTable";
import AccountDetailsComponent from "../components/AccountDetailsComponent";
import EditAccountComponent from "../components/EditAccountComponent";
import useTransactionEntities from "../../../utils/hooks/useTransactionEntities";
import useAccount from "../../../utils/hooks/useAccount";
import groupBy from "../../../utils/transactionsGroupBy";
import {Account} from "../../../models/accounts/account";
import {useNavigate, useParams} from "react-router-dom";
import {deflateRaw} from "zlib";
import {TotalAccountsDetails} from "../components/TotalAccountsDetails";

export default observer(function AccountDetailsPage() {
    const navigate = useNavigate()
    const { accountId } = useParams<{ accountId: string }>();
    const [editMode, setEditMode] = useState(false)
    const [groupCriterion, setGroupCriterion] = useState('day')
    const [collapsedGroups, setCollapsedGroups] = useState<string[]>([])
    const {accountStore, transactionStore, transactionEntityStore} = useStore()
    const [account, setAccount] = useState<Account>()
    const transactionEntities = useTransactionEntities(account)
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))

    const aggregateAccounts = (accounts: Account[]) => {
        return new Account(
            '',
            'Total',
            {
                amount: accounts
                .flatMap(a => a.transactions)
                    .reduce((total, transaction) => total + transaction.transactionAmount.amount, 0),
                currency: accounts[0].balance.currency
            },
            accounts.flatMap(a => a.transactions)
                .sort((a, b) => Number(new Date(b.transactionDateUtc)) - Number(new Date(a.transactionDateUtc))),
            'Total'
        )
    }

    useEffect(() => {
        const loadAccounts = async () => await accountStore.loadAccounts()

        if (accountId && accountStore.getAccount(accountId)) {
            setAccount(accountStore.getAccount(accountId))
        } else if(accountId && !accountStore.getAccount(accountId)){
            loadAccounts().then(() => setAccount(accountStore.getAccount(accountId)))
        } else {
            loadAccounts().then(() => {
                accountStore.accounts.length > 0 ?
                    setAccount(aggregateAccounts(accountStore.accounts)) :
                    navigate('/accounts')
            })
        }
    }, [accountId, accountStore, navigate])

    const resetCollapse = () => collapsedGroups.length > 0 && setCollapsedGroups([])

    const handleGroupChange = (groupKey: string) => {
        setGroupCriterion(groupKey)
        resetCollapse()
    }

    const groupedTransactions = account &&
        groupBy(account.transactions, groupCriterion)

    return (
        <AppOverlay>
            {!(transactionEntities.length > 0 && groupedTransactions && !accountStore.loading && account && !transactionStore.loading && !transactionEntityStore.loading) ?
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
                    {!accountId ?
                        <TotalAccountsDetails
                            account={account}
                            groupDropdownProps={{groupCriterion: groupCriterion, handleGroupChange: handleGroupChange}}
                        /> :
                        !editMode ?
                            <AccountDetailsComponent
                                account={account}
                                groupDropdownProps={{groupCriterion: groupCriterion, handleGroupChange: handleGroupChange}}
                                setEditMode={setEditMode}
                            /> :
                            <EditAccountComponent
                                account={account}
                                groupDropdownProps={{groupCriterion: groupCriterion, handleGroupChange: handleGroupChange}}
                                setEditMode={setEditMode}
                                setAccount={setAccount}
                            />
                    }
                    <Grid item xs={12} sx={{overflow: 'hidden', maxHeight: '50%'}}>
                        <TransactionsTable
                            groupCriterion={groupCriterion}
                            collapsedGroups={collapsedGroups}
                            setCollapsedGroups={setCollapsedGroups}
                            groupedTransactions={groupedTransactions}
                            editMode={editMode}
                        />
                    </Grid>
                </Grid>
            }
        </AppOverlay>
    );
})