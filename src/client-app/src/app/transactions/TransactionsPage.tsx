import AppOverlay from "../../components/appOverlay/AppOverlay";
import useTitle from "../../utils/hooks/useTitle";
import {observer} from "mobx-react-lite";
import React, {useEffect, useState} from "react";
import {useStore} from "../../stores/store";
import {Grid, useMediaQuery} from "@mui/material";
import theme from "../theme";
import TransactionsTable from "../../components/transactionsTable/TransactionsTable";
import {useNavigate, useParams} from "react-router-dom";
import {Account} from "../../models/accounts/account";
import useTransactionEntities from "../../utils/hooks/useTransactionEntities";
import useCategories from "../../utils/hooks/useCategories";
import groupBy from "../../utils/transactionsGroupBy";
import GroupDropdown from "../../components/transactionsTable/GroupDropdown";

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

    useEffect(() => {
        const loadTransactions = async () => await transactionStore.loadTransactions()

        loadTransactions()

    }, [])

    const resetCollapse = () => collapsedGroups.length > 0 && setCollapsedGroups([])

    const handleGroupChange = (groupKey: string) => {
        setGroupCriterion(groupKey)
        resetCollapse()
    }

    const groupedTransactions = transactionStore.transactions &&
        groupBy(transactionStore.transactions, groupCriterion)

    useEffect(() => {
        layoutStore.setActiveSectionIndex(7)
    }, [])

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
                        <Grid item xs={12} marginBottom={3} sx={{
                            display: 'flex',
                            alignItems:'center',
                            justifyContent: 'center',
                            maxHeight: '10%'
                        }}>
                            <GroupDropdown groupCriterion={groupCriterion} handleGroupChange={handleGroupChange} />
                        </Grid>
                        <Grid item xs={12} sx={{overflow: 'hidden', maxHeight: '80%'}}>
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