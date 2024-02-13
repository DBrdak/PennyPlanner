import AppOverlay from "../../../components/appOverlay/AppOverlay";
import theme from "../../theme";
import {CircularProgress, Grid} from "@mui/material"
import * as Yup from 'yup'
import {AddIncomeForm} from "./AddIncomeForm";
import {useStore} from "../../../stores/store";
import {useEffect, useState} from "react";
import {TransactionEntity} from "../../../models/transactionEntities/transactionEntity";
import {TransactionCategory} from "../../../models/transactionCategories/transactionCategory";
import {AddIncomeTransactionCommand} from "../../../models/requests/addIncomeTransactionCommand";

export function AddIncomePage() {
    const {accountStore, transactionEntityStore, transactionStore, categoryStore} = useStore()
    const [senders, setSenders] = useState<TransactionEntity[]>([])
    const [categories, setCategories] = useState<TransactionCategory[]>([])

    useEffect(() => {
        const loadAll = async () => {
            await accountStore.loadAccounts()
            await transactionEntityStore.loadTransactionEntities()
            await categoryStore.loadCategories()
        }

        loadAll().then(() => {
            setSenders(transactionEntityStore.transactionEntities
                .filter(te => te.transactionEntityType.toLowerCase() === "sender"))
            setCategories(categoryStore.categories
                .filter(c => c.type.toLowerCase() === 'income'))
        })
    }, [])

    async function handleFormSubmit(values: AddIncomeTransactionCommand) {
        await transactionStore.addTransaction(values)
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
            }}>
                {
                    accountStore.loading || accountStore.accounts.length < 1  || transactionEntityStore.loading || categoryStore.loading ?
                        <CircularProgress/> :
                        <AddIncomeForm
                            accounts={accountStore.accounts}
                            senders={senders}
                            categories={categories}
                            handleFormSubmit={handleFormSubmit}
                        />
                }
            </Grid>
        </AppOverlay>
    );
}