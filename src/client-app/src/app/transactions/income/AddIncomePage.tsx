import AppOverlay from "../../../components/appOverlay/AppOverlay";
import theme from "../../theme";
import {CircularProgress, Grid, IconButton, Typography} from "@mui/material"
import {AddIncomeForm} from "./components/AddIncomeForm";
import {useStore} from "../../../stores/store";
import React, {useEffect, useState} from "react";
import {AddIncomeTransactionCommand} from "../../../models/requests/addIncomeTransactionCommand";
import useTitle from "../../../utils/hooks/useTitle";
import {observer} from "mobx-react-lite";
import NewIncomesTable from "./components/NewIncomesTable";
import {NoAccountMessage} from "../components/NoAccountMessage";
import {Undo} from "@mui/icons-material";
import {useNavigate} from "react-router-dom";

export default observer(function AddIncomePage() {
    const {accountStore, transactionEntityStore, transactionStore, categoryStore} = useStore()
    const [senderNames, setSenderNames] = useState<string[]>([])
    const [categoryValues, setCategoryValues] = useState<string[]>([])
    const [newIncomes, setNewIncomes] = useState<AddIncomeTransactionCommand[]>([])
    const navigate = useNavigate()
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
        })
    }, [accountStore, categoryStore, transactionEntityStore, transactionStore])

    async function handleFormSubmit(values: AddIncomeTransactionCommand) {
        await transactionStore.addTransaction(new AddIncomeTransactionCommand(values)).then(() => {
            !senderNames.some(name => name === values.senderName) &&
            setSenderNames([...senderNames, values.senderName])
            !categoryValues.some(value => value === values.categoryValue) &&
            setCategoryValues([...categoryValues, values.categoryValue])
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
                position: 'relative'
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
                                        handleFormSubmit={handleFormSubmit}
                                    />
                                </Grid>
                                <Grid item xs={12} md={6} sx={{height: '100%'}}>
                                    <NewIncomesTable
                                        newIncomes={newIncomes}
                                    />
                                </Grid>
                            </>
                }
            </Grid>
        </AppOverlay>
    );
})