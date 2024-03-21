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
import TransactionsTable from "../../../components/transactionsTable/TransactionsTable";
import AccountDetailsComponent from "../components/details/AccountDetailsComponent";
import EditAccountComponent from "../components/edit/EditAccountComponent";
import useTransactionEntities from "../../../utils/hooks/useTransactionEntities";
import groupBy from "../../../utils/transactionsGroupBy";
import {Account} from "../../../models/accounts/account";
import {useNavigate, useParams} from "react-router-dom";
import useTitle from "../../../utils/hooks/useTitle";
import useCategories from "../../../utils/hooks/useCategories";
import useAuthProvider from "../../../utils/hooks/useAuthProvider";

export default observer(function AccountDetailsPage() {
    useAuthProvider()
    const navigate = useNavigate()
    const { accountId } = useParams<{ accountId: string }>();
    const [editMode, setEditMode] = useState(false)
    const [groupCriterion, setGroupCriterion] = useState('day')
    const [collapsedGroups, setCollapsedGroups] = useState<string[]>([])
    const {accountStore, categoryStore, transactionStore, transactionEntityStore} = useStore()
    const [account, setAccount] = useState<Account>()
    useTransactionEntities(account);
    useCategories(account);
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))

    useTitle(undefined, account?.name)

    useEffect(() => {
        const loadAccounts = async () => await accountStore.loadAccounts()

        if (accountId && accountStore.getAccount(accountId)) {
            setAccount(accountStore.getAccount(accountId))
        } else if(accountId && !accountStore.getAccount(accountId)){
            loadAccounts().then(() => setAccount(accountStore.getAccount(accountId)))
        } else {
            navigate('/accounts')
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
            {!(groupedTransactions &&
                !accountStore.loading &&
                account &&
                !transactionStore.loading &&
                !transactionEntityStore.loading &&
                !categoryStore.loading) ?
                <CircularProgress color={'secondary'} />
                :
                <Grid container sx={{
                    height:'100%',
                    padding: isMobile ? 3 : 4,
                    margin: 0,
                    backgroundColor: theme.palette.background.paper,
                    borderRadius: '20px',
                    overflow: isMobile ? 'auto' : 'hidden',
                    maxWidth: '1920px'
                }}>
                    {!editMode ?
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
                    <Grid item xs={12} sx={{overflow: 'hidden', maxHeight: '60%'}}>
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