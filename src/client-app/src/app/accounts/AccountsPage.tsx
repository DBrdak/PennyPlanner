import AppOverlay from "../../components/appOverlay/AppOverlay";
import {TilesLayout} from "../../components/tilesLayout/TilesLayout";
import {AccountTile} from "./components/tiles/AccountTile";
import {useStore} from "../../stores/store";
import {useEffect} from "react";
import {CircularProgress, useMediaQuery} from "@mui/material";
import theme from "../theme";
import {observer} from "mobx-react-lite";
import {NewAccountTile} from "./components/tiles/NewAccountTile";
import TotalAccountsTile from "./components/tiles/TotalAccountsTile";
import useTitle from "../../utils/hooks/useTitle";
import Tile from "../../models/layout/tile";
import ValidationConstants from "../../utils/constants/validationConstants";

export default observer (function AccountsPage() {
    const {accountStore} = useStore()
    const cols = 6

    useTitle('Accounts')

    useEffect(() => {
        const load = async () => await accountStore.loadAccounts()
        load()
    }, [])

    const tiles: Tile[] = []

    if(accountStore.accounts.flatMap(a => a.transactions).length > 0){

        tiles.push(
            {
                cols: 12, height: '50%', content:
                    <TotalAccountsTile />
            },
        )
    }

    tiles.push(...[
        ...accountStore.accounts.map(account => (
            {
                cols: cols, height: '50%', content:
                    <AccountTile account={account}/>
            }
        )),
        {
            cols: cols, height: '50%', content:
                <NewAccountTile/>
        },
    ])

    if(accountStore.accounts.length >= ValidationConstants.accountsLimit) {
        tiles.pop()
    }

    return (
        <AppOverlay>
            {accountStore.loading ?
                <CircularProgress color={'secondary'} /> :
                <TilesLayout tiles={tiles} />
            }
        </AppOverlay>
    );
})