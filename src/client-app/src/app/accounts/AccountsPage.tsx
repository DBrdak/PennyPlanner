import AppOverlay from "../../components/appOverlay/AppOverlay";
import {TilesLayout} from "../../components/tilesLayout/TilesLayout";
import {AccountTile} from "./components/AccountTile";
import {useStore} from "../../stores/store";
import {useEffect} from "react";
import {CircularProgress, useMediaQuery} from "@mui/material";
import theme from "../theme";
import {observer} from "mobx-react-lite";
import {NewAccountTile} from "./components/NewAccountTile";
import TotalAccountsTile from "./components/TotalAccountsTile";

export default observer (function AccountsPage() {
    const isUwhd = useMediaQuery(theme.breakpoints.up('xl'))
    const {accountStore} = useStore()
    const cols = isUwhd ? 4 : 6

    useEffect(() => {
        const load = async () => await accountStore.loadAccounts()
        load()
    }, [])

    const tiles = [
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
    ]

    if(accountStore.accounts.flatMap(a => a.transactions).length > 0){
        tiles.pop()

        tiles.push(
            {
                cols: cols, height: '50%', content:
                    <TotalAccountsTile />
            },
        )

        tiles.push(
            {
                cols: cols, height: '50%', content:
                    <NewAccountTile/>
            },
        )
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