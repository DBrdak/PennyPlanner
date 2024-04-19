import AppOverlay from "../../../components/appOverlay/AppOverlay";
import useTitle from "../../../utils/hooks/useTitle";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../stores/store";
import React, {useEffect, useState} from "react";
import {CircularProgress, Grid, IconButton, useMediaQuery} from "@mui/material";
import theme from "../../theme";
import {AddInternalTransactionCommand} from "../../../models/requests/transactions/addInternalTransactionCommand";
import AddInternalTransactionForm from "./components/AddInternalTransactionForm";
import NewInternalTransactionsTable from "./components/NewInternalTransactionsTable";
import {NoAccountMessage} from "../components/NoAccountMessage";
import {Undo} from "@mui/icons-material";
import {useNavigate} from "react-router-dom";
import useAuthProvider from "../../../utils/hooks/useAuthProvider";

export default observer (function AddInternalTransactionPage() {
    useAuthProvider()
    const {accountStore, transactionStore} = useStore()
    const [newTransactions, setNewTransactions] = useState<AddInternalTransactionCommand[]>([])
    const navigate = useNavigate()
    const isMobile = useMediaQuery(theme.breakpoints.down('sm'))
    useTitle('Internal')

    useEffect(() => {
        const loadaccounts = async () => {
            await accountStore.loadAccounts()
        }

        loadaccounts()
    }, [accountStore, transactionStore])

    async function handleFormSubmit(values: AddInternalTransactionCommand) {
        await transactionStore.addTransaction(new AddInternalTransactionCommand(values)).then(() => {
            setNewTransactions([...newTransactions, values])
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
                    accountStore.loading  ?
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
                                    <AddInternalTransactionForm
                                        accounts={accountStore.accounts}
                                        handleFormSubmit={handleFormSubmit}
                                    />
                                </Grid>
                                {
                                    !isMobile &&
                                        <Grid item xs={12} md={6} sx={{height: '100%'}}>
                                            <NewInternalTransactionsTable
                                                newTransactions={newTransactions}
                                            />
                                        </Grid>
                                }
                            </>
                }
            </Grid>
        </AppOverlay>
    );
})