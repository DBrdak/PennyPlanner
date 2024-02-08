import AppOverlay from "../../components/appOverlay/AppOverlay";
import {TilesLayout} from "../../components/tilesLayout/TilesLayout";
import {observer} from "mobx-react-lite";
import useTitle from "../../utils/hooks/useTitle";
import Tile from "../../models/layout/tile";
import {TransactionsPreviewTile} from "./components/tiles/TransactionsPreviewTile";
import {BudgetPreviewTile} from "./components/tiles/BudgetPreviewTile";
import {GoalsPreviewTile} from "./components/tiles/GoalsPreviewTile";
import {AccountsPreviewTile} from "./components/tiles/AccountsPreviewTile";
import {TransactionEntitiesPreviewTile} from "./components/tiles/TransactionEntitiesPreviewTile";
import {useStore} from "../../stores/store";
import {CircularProgress} from "@mui/material";
import {useEffect} from "react";

export default observer(function HomePage() {
    useTitle('Home')
    const {accountStore, transactionStore, transactionEntityStore} = useStore()

    useEffect(() => {
        const loadAll = async () => {
            accountStore.loadAccounts()
            transactionStore.loadTransactions()
            transactionEntityStore.loadTransactionEntities()
        }

        loadAll()
    }, [])

    const tiles: () => Tile[] = () =>  ([
        {
            cols: 8, height: '60%', content: <TransactionsPreviewTile
                transactions={transactionStore.transactions}
                accounts={accountStore.accounts}
                transactionEntities={transactionEntityStore.transactionEntities}
            />
        },
        {
            cols: 4, height: '60%', content: <BudgetPreviewTile />
        },
        {
            cols: 4, height: '40%', content: <GoalsPreviewTile />
        },
        {
            cols: 4, height: '40%', content: <AccountsPreviewTile />
        },
        {
            cols: 4, height: '40%', content: <TransactionEntitiesPreviewTile />
        }
    ])

    return (
        <AppOverlay>
            { transactionStore.loading || accountStore.loading || transactionEntityStore.loading ?
                <CircularProgress color={'secondary'} />
                :
                <TilesLayout tiles={tiles()}/>
            }
        </AppOverlay>
    );
})