import AppOverlay from "../../../components/appOverlay/AppOverlay";
import theme from "../../theme";
import {CircularProgress, Grid} from "@mui/material"
import {AddIncomeForm} from "./components/AddIncomeForm";
import {useStore} from "../../../stores/store";
import {useEffect, useState} from "react";
import {AddIncomeTransactionCommand} from "../../../models/requests/addIncomeTransactionCommand";
import useTitle from "../../../utils/hooks/useTitle";
import {observer} from "mobx-react-lite";
import NewIncomesTable from "./components/NewIncomesTable";

export default observer(function AddIncomePage() {
    const {accountStore, transactionEntityStore, transactionStore, categoryStore} = useStore()
    const [senderNames, setSenderNames] = useState<string[]>([])
    const [categoryValues, setCategoryValues] = useState<string[]>([])
    const [newIncomes, setNewIncomes] = useState<AddIncomeTransactionCommand[]>([])
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
                alignItems: 'center'
            }}>
                {
                    accountStore.loading  || transactionEntityStore.loading || categoryStore.loading ?
                        <CircularProgress/> :
                        <>
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