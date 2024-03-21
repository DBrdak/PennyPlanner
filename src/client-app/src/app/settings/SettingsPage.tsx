import AppOverlay from "../../components/appOverlay/AppOverlay";
import {TilesLayout} from "../../components/tilesLayout/TilesLayout";
import {observer} from "mobx-react-lite";
import useTitle from "../../utils/hooks/useTitle";
import Tile from "../../models/layout/tile";
import {TransactionCategoriesTile} from "./components/tiles/TransactionCategoriesTile";
import {TransactionEntitiesTile} from "./components/tiles/TransactionEntitiesTile";
import {RecurringTransactionsTile} from "./components/tiles/RecurringTransactionsTile";
import {CustomizeTile} from "./components/tiles/CustomizeTile";
import useAuthProvider from "../../utils/hooks/useAuthProvider";

export default observer(function SettingsPage() {
    useAuthProvider()
    useTitle('Settings')

    const tiles: Tile[] = [
        {height: '50%', cols: 6, content: <TransactionCategoriesTile/>},
        {height: '50%', cols: 6, content: <TransactionEntitiesTile />},
        {height: '50%', cols: 6, content: <RecurringTransactionsTile/>},
        {height: '50%', cols: 6, content: <CustomizeTile/>},
    ]

    return (
        <AppOverlay>
            <TilesLayout tiles={tiles} />
        </AppOverlay>
    );
})