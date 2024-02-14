import AppOverlay from "../../../components/appOverlay/AppOverlay";
import useTitle from "../../../utils/hooks/useTitle";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../stores/store";
import {useEffect, useState} from "react";
import {CircularProgress, Grid, Typography} from "@mui/material";
import theme from "../../theme";
import {AddInternalTransactionCommand} from "../../../models/requests/addInternalTransactionCommand";
import {AddInternalTransactionForm} from "./components/AddInternalTransactionForm";
import NewInternalTransactionsTable from "./components/NewInternalTransactionsTable";

export default observer (function AddInternalTransactionPage() {
    const {accountStore, transactionStore} = useStore()
    const [newTransactions, setNewTransactions] = useState<AddInternalTransactionCommand[]>([])
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
                alignItems: 'center'
            }}>
                {
                    accountStore.loading  ?
                        <CircularProgress/> :
                        accountStore.accounts.length < 0 ?
                            <Typography variant={'h3'}>
                                Please add account first
                            </Typography> :
                            <>
                                <Grid item xs={12} md={6} sx={{height: '100%'}}>
                                    <AddInternalTransactionForm
                                        accounts={accountStore.accounts}
                                        handleFormSubmit={handleFormSubmit}
                                    />
                                </Grid>
                                <Grid item xs={12} md={6} sx={{height: '100%'}}>
                                    <NewInternalTransactionsTable
                                        newTransactions={newTransactions}
                                    />
                                </Grid>
                            </>
                }
            </Grid>
        </AppOverlay>
    );
})