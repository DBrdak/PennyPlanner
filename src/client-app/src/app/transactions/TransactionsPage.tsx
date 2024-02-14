import AppOverlay from "../../components/appOverlay/AppOverlay";
import useTitle from "../../utils/hooks/useTitle";
import {observer} from "mobx-react-lite";
import React, {useEffect, useState} from "react";
import {useStore} from "../../stores/store";
import {Button, ButtonGroup, Grid, IconButton, Typography, useMediaQuery} from "@mui/material";
import theme from "../theme";
import TransactionsTable from "../../components/transactionsTable/TransactionsTable";
import {useNavigate, useParams} from "react-router-dom";
import {Account} from "../../models/accounts/account";
import useTransactionEntities from "../../utils/hooks/useTransactionEntities";
import useCategories from "../../utils/hooks/useCategories";
import groupBy from "../../utils/transactionsGroupBy";
import GroupDropdown from "../../components/transactionsTable/GroupDropdown";
import {North, South, SyncAlt as SyncAltIcon, SyncAlt} from "@mui/icons-material";
import useAccount from "../../utils/hooks/useAccount";

export default observer (function TransactionsPage() {
    useTitle('Transactions')
    const {layoutStore} = useStore()
    const navigate = useNavigate()
    const { accountId } = useParams<{ accountId: string }>();
    const [editMode, setEditMode] = useState(false)
    const [groupCriterion, setGroupCriterion] = useState('day')
    const [collapsedGroups, setCollapsedGroups] = useState<string[]>([])
    const {accountStore, categoryStore, transactionStore, transactionEntityStore} = useStore()
    const transactionEntities = useTransactionEntities()
    const categories = useCategories()
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
    const buttons =[
        { icon: <South color={'success'} />, name: 'Income', path: '/transactions/income' },
        { icon: <North color={'error'} />, name: 'Outcome', path: '/transactions/outcome' },
        { icon: <SyncAltIcon color={'info'} />, name: 'Internal Transaction', path: '/transactions/internal' },
    ]

    useEffect(() => {
        const loadAll = async () => {
            await accountStore.loadAccounts()
            await transactionStore.loadTransactions()
        }

        loadAll()

    }, [transactionStore])

    const resetCollapse = () => collapsedGroups.length > 0 && setCollapsedGroups([])

    const handleGroupChange = (groupKey: string) => {
        setGroupCriterion(groupKey)
        resetCollapse()
    }

    const groupedTransactions = transactionStore.transactions &&
        groupBy(transactionStore.transactions, groupCriterion)

    return (
        <AppOverlay>
            <Grid container sx={{
                height:'100%',
                padding: isMobile ? 1 : 2,
                margin: 0,
                backgroundColor: theme.palette.background.paper,
                borderRadius: '20px',
                overflow:'auto'
            }}>
                {groupedTransactions &&
                    <>
                        <Grid item xs={12} md={6} sx={{
                            display: 'flex',
                            alignItems:'center',
                            justifyContent: 'center',
                            maxHeight: '15%',
                            marginBottom: 3
                        }}>
                            <GroupDropdown groupCriterion={groupCriterion} handleGroupChange={handleGroupChange} noAccount />
                        </Grid>
                        <Grid item xs={12} md={6} sx={{
                            display: 'flex',
                            alignItems:'center',
                            justifyContent: 'center',
                            maxHeight: '15%',
                            flexDirection: 'column'
                        }}>
                            <Typography variant={'h6'}>Add New Transaction</Typography>
                            <ButtonGroup fullWidth sx={{justifyContent: 'space-between'}}>
                                {buttons.map((button, index) =>(
                                    <IconButton key={index} onClick={() => navigate(button.path)} sx={{
                                        width: `calc(100% / ${buttons.length})`,
                                        flexDirection: 'column',
                                        borderRadius: 0
                                    }}>
                                        {button.icon}
                                        <Typography variant={'caption'}>
                                            {button.name}
                                        </Typography>
                                    </IconButton>
                                    ))}
                            </ButtonGroup>
                        </Grid>
                        <Grid item xs={12} sx={{overflow: 'hidden', maxHeight: '70%'}}>
                            <TransactionsTable
                                groupCriterion={groupCriterion}
                                collapsedGroups={collapsedGroups}
                                setCollapsedGroups={setCollapsedGroups}
                                groupedTransactions={groupedTransactions}
                                editMode={editMode}
                            />
                        </Grid>
                    </>
                }
            </Grid>
        </AppOverlay>
    );
})