import AppOverlay from "../../components/appOverlay/AppOverlay";
import {TilesLayout} from "../../components/tilesLayout/TilesLayout";
import {observer} from "mobx-react-lite";
import useTitle from "../../utils/hooks/useTitle";
import Tile from "../../models/layout/tile";
import {TransactionsViewTile} from "./components/tiles/TransactionsViewTile";
import {BudgetPreviewTile} from "./components/tiles/BudgetPreviewTile";
import {GoalsPreviewTile} from "./components/tiles/GoalsPreviewTile";
import {AccountsPreviewTile} from "./components/tiles/AccountsPreviewTile";
import {TransactionEntitiesPreviewTile} from "./components/tiles/TransactionEntitiesPreviewTile";

export default observer(function HomePage() {

    useTitle('Home')

    const tiles: Tile[] = [
        {
            cols: 8, height: '25%', content: <TransactionsViewTile />
        },
        {
            cols: 4, height: '25%', content: <BudgetPreviewTile />
        },
        {
            cols: 4, height: '25%', content: <GoalsPreviewTile />
        },
        {
            cols: 4, height: '25%', content: <AccountsPreviewTile />
        },
        {
            cols: 4, height: '25%', content: <TransactionEntitiesPreviewTile />
        }
    ]

    return (
        <AppOverlay>
            <TilesLayout tiles={tiles} />
        </AppOverlay>
    );
})