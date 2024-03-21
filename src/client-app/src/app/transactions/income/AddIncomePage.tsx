import Undo from "@mui/icons-material/Undo";
import {CircularProgress, Grid, IconButton, useMediaQuery} from "@mui/material";
import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import AppOverlay from "../../../components/appOverlay/AppOverlay";
import { AddIncomeTransactionCommand } from "../../../models/requests/transactions/addIncomeTransactionCommand";
import { useStore } from "../../../stores/store";
import useTitle from "../../../utils/hooks/useTitle";
import theme from "../../theme";
import { NoAccountMessage } from "../components/NoAccountMessage";
import { AddIncomeForm } from "./components/AddIncomeForm";
import NewIncomesTable from "./components/NewIncomesTable";
import NewInternalTransactionsTable from "../internal/components/NewInternalTransactionsTable";
import useAuthProvider from "../../../utils/hooks/useAuthProvider";

export default observer(function AddIncomePage() {
    useAuthProvider()
    const {accountStore, transactionEntityStore, transactionStore, categoryStore} = useStore()
    const [senderNames, setSenderNames] = useState<string[]>([])
    const [categoryValues, setCategoryValues] = useState<string[]>([])
    const [subcategoryValues, setSubcategoryValues] = useState<string[]>([])
    const [newIncomes, setNewIncomes] = useState<AddIncomeTransactionCommand[]>([])
    const navigate = useNavigate()
    const isMobile = useMediaQuery(theme.breakpoints.down('sm'))
    useTitle('Income')

    useEffect(() => {
        const loadAll = async () => {
            await accountStore.loadAccounts()
            await transactionEntityStore.loadTransactionEntities()
            await categoryStore.loadCategories()
        }

        loadAll().then(() => {
            setSenderNames(transactionEntityStore.transactionEntities
                .filter(te => te.transactionEntityType.toLowerCase() === "sender").map(te => te.name))
            setCategoryValues(categoryStore.categories
                .filter(c => c.type.toLowerCase() === 'income').map(c => c.value))
            setSubcategoryValues(categoryStore.categories
                .filter(c => c.type.toLowerCase() === 'income').flatMap(c => c.subcategories)
                .map(s => s.value))
        })
    }, [accountStore, categoryStore, transactionEntityStore, transactionStore])

    async function handleFormSubmit(values: AddIncomeTransactionCommand) {
        await transactionStore.addTransaction(new AddIncomeTransactionCommand(values)).then(() => {
            !senderNames.some(name => name === values.senderName) &&
                setSenderNames([...senderNames, values.senderName])
            !categoryValues.some(value => value === values.categoryValue) &&
                setCategoryValues([...categoryValues, values.categoryValue])
            !subcategoryValues.some(value => value === values.subcategoryValue) &&
                setSubcategoryValues([...subcategoryValues, values.subcategoryValue])
            setNewIncomes([...newIncomes, values])
        })
    }

    return (
        <AppOverlay>
            <Grid container sx={{
                height:'100%',
                width: '100%',
                padding: 3,
                margin: 0,
                backgroundColor: theme.palette.background.paper,
                borderRadius: '20px',
                overflow: 'auto',
                justifyContent: 'center',
                alignItems: 'center',
                position: 'relative',
                maxWidth: '1920px'
            }}>
                {
                    accountStore.loading  || transactionEntityStore.loading || categoryStore.loading ?
                        <CircularProgress/> :
                        accountStore.accounts.length < 1 ?
                            <NoAccountMessage /> :
                            <>
                                <IconButton onClick={() => navigate('/transactions')}
                                            sx={{
                                                position: 'absolute',
                                                top: '1px', left: '1px',
                                                width: '5rem',
                                                height: '5rem'
                                            }}>
                                    <Undo fontSize={'large'} />
                                </IconButton>
                                <Grid item xs={12} md={6} sx={{height: '100%'}}>
                                    <AddIncomeForm
                                        accounts={accountStore.accounts}
                                        senders={senderNames}
                                        categories={categoryValues}
                                        subcategories={subcategoryValues}
                                        handleFormSubmit={handleFormSubmit}
                                    />
                                </Grid>
                                {
                                    !isMobile &&
                                    <Grid item xs={12} md={6} sx={{height: '100%'}}>
                                        <NewIncomesTable
                                            newIncomes={newIncomes}
                                        />
                                    </Grid>
                                }
                            </>
                }
            </Grid>
        </AppOverlay>
    );
})