import AppOverlay from "../../components/appOverlay/AppOverlay";
import useTitle from "../../utils/hooks/useTitle";
import {observer} from "mobx-react-lite";
import React, {useEffect, useState} from "react";
import {useStore} from "../../stores/store";
import {
    Button,
    ButtonGroup,
    CircularProgress,
    Divider,
    Grid,
    IconButton,
    Typography,
    useMediaQuery
} from "@mui/material";
import theme from "../theme";
import TransactionsTable from "../../components/transactionsTable/TransactionsTable";
import {useNavigate} from "react-router-dom";
import useTransactionEntities from "../../utils/hooks/useTransactionEntities";
import useCategories from "../../utils/hooks/useCategories";
import groupBy from "../../utils/transactionsGroupBy";
import GroupDropdown from "../../components/transactionsTable/GroupDropdown";
import {North, South, SyncAlt as SyncAltIcon, Undo} from "@mui/icons-material";

export default observer (function TransactionsPage() {
    useTitle('Transactions')
    const navigate = useNavigate()
    const [groupCriterion, setGroupCriterion] = useState('day')
    const [collapsedGroups, setCollapsedGroups] = useState<string[]>([])
    const {accountStore, categoryStore, transactionStore, transactionEntityStore} = useStore()
    useTransactionEntities()
    useCategories()
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
                overflow:'auto',
                position: 'relative',
                maxWidth: '1920px'
            }}>
                {groupedTransactions &&
                    <>

                        <IconButton onClick={() => navigate('/home')}
                                    sx={{
                                        position: 'absolute',
                                        top: '1px', left: '1px',
                                        width: '5rem',
                                        height: '5rem'
                                    }}>
                            <Undo fontSize={'large'} />
                        </IconButton>
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
                            <ButtonGroup fullWidth sx={{justifyContent: 'space-between'}}>
                                {buttons.map((button, index) =>(
                                    <Button key={index} onClick={() => navigate(button.path)} variant={'outlined'} color={'inherit'} sx={{
                                        width: `calc(100% / ${buttons.length})`,
                                        flexDirection: 'column',
                                        borderRadius: '20px'
                                    }}>
                                        {button.icon}
                                        <Typography variant={'caption'}>
                                            Add {button.name}
                                        </Typography>
                                    </Button>
                                    ))}
                            </ButtonGroup>
                        </Grid>
                        <Grid item xs={12}>
                            <Divider variant={'middle'} />
                        </Grid>
                        <Grid item xs={12} sx={{overflow: 'hidden', height: '70%'}}>
                            {
                                transactionStore.loading ?
                                    <Grid item xs={12} sx={{
                                        display: 'flex',
                                        justifyContent: 'center',
                                        alignItems: 'center'
                                    }}>
                                        <CircularProgress />
                                    </Grid>
                                :
                                <TransactionsTable
                                    groupCriterion={groupCriterion}
                                    collapsedGroups={collapsedGroups}
                                    setCollapsedGroups={setCollapsedGroups}
                                    groupedTransactions={groupedTransactions}
                                    editMode={true}
                                />
                            }
                        </Grid>
                    </>
                }
            </Grid>
        </AppOverlay>
    );
})