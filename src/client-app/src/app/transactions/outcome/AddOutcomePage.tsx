import AppOverlay from "../../../components/appOverlay/AppOverlay";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../stores/store";
import React, {useEffect, useState} from "react";
import useTitle from "../../../utils/hooks/useTitle";
import {Button, CircularProgress, Grid, IconButton, Typography} from "@mui/material";
import theme from "../../theme";
import {AddOutcomeTransactionCommand} from "../../../models/requests/addOutcomeTransactionCommand";
import NewOutcomesTable from "./components/NewOutcomesTable";
import {AddOutcomeForm} from "./components/AddOutcomeForm";
import {ArrowForward, Undo} from "@mui/icons-material";
import {NoAccountMessage} from "../components/NoAccountMessage";
import {useNavigate} from "react-router-dom";

export default observer (function AddOutcomePage() {
    const {accountStore, transactionEntityStore, transactionStore, categoryStore} = useStore()
    const [recipientNames, setRecipientNames] = useState<string[]>([])
    const [categoryValues, setCategoryValues] = useState<string[]>([])
    const [newOutcomes, setNewOutcomes] = useState<AddOutcomeTransactionCommand[]>([])
    const navigate = useNavigate()
    useTitle('Outcome')

    useEffect(() => {
        const loadAll = async () => {
            await accountStore.loadAccounts()
            await transactionEntityStore.loadTransactionEntities()
            await categoryStore.loadCategories()
        }

        loadAll().then(() => {
            setRecipientNames(transactionEntityStore.transactionEntities
                .filter(te => te.transactionEntityType.toLowerCase() === "recipient").map(te => te.name))
            setCategoryValues(categoryStore.categories
                .filter(c => c.type.toLowerCase() === 'outcome').map(c => c.value))
        })
    }, [accountStore, categoryStore, transactionEntityStore, transactionStore])

    async function handleFormSubmit(values: AddOutcomeTransactionCommand) {
        await transactionStore.addTransaction(new AddOutcomeTransactionCommand(values)).then(() => {
            !recipientNames.some(name => name === values.recipientName) &&
            setRecipientNames([...recipientNames, values.recipientName])
            !categoryValues.some(value => value === values.categoryValue) &&
            setCategoryValues([...categoryValues, values.categoryValue])
            setNewOutcomes([...newOutcomes, values])
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
                                    <AddOutcomeForm
                                        accounts={accountStore.accounts}
                                        recipients={recipientNames}
                                        categories={categoryValues}
                                        handleFormSubmit={handleFormSubmit}
                                    />
                                </Grid>
                                <Grid item xs={12} md={6} sx={{height: '100%'}}>
                                    <NewOutcomesTable
                                        newOutcomes={newOutcomes}
                                    />
                                </Grid>
                            </>
                }
            </Grid>
        </AppOverlay>
    );
})