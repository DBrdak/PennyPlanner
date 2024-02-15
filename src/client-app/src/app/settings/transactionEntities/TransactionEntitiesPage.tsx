import AppOverlay from "../../../components/appOverlay/AppOverlay";
import {CircularProgress, Grid, Typography} from "@mui/material";
import theme from "../../theme";
import TilePaper from "../../../components/tilesLayout/TilePaper";
import {TilesLayout} from "../../../components/tilesLayout/TilesLayout";
import {observer} from "mobx-react-lite";
import useTransactionEntities from "../../../utils/hooks/useTransactionEntities";
import {useStore} from "../../../stores/store";
import {TransactionEntityTile} from "./components/TransactionEntityTile";
import {AddTransactionEntityTile} from "./components/AddTransactionEntityTile";

export default observer (function TransactionEntitiesPage() {
    const {transactionEntityStore} = useStore()
    const transactionEntities = useTransactionEntities()

    const handleDelete = (transactionEntityId: string) => {

    }

    const handleEdit = (transactionEntityId: string) => {

    }

    return (
        <AppOverlay>
            <Grid container sx={{
                height:'100%',
                padding: 2,
                margin: 0,
                backgroundColor: theme.palette.background.paper,
                borderRadius: '20px',
                overflow:'auto',
            }}>
                {
                    transactionEntityStore.loading ?
                        <Grid item xs={12} height={'100%'} sx={{display: 'flex', alignItems: 'center', justifyContent: 'center'}}>
                            <CircularProgress />
                        </Grid> :
                        <>
                        {transactionEntities.map(transactionEntity => (
                                <TransactionEntityTile
                                    key={transactionEntity.transactionEntityId}
                                    transactionEntity={transactionEntity}
                                    onDelete={handleDelete}
                                    onEdit={handleEdit}
                                />
                            ))}
                            <AddTransactionEntityTile />
                        </>
                }
            </Grid>
        </AppOverlay>
    );
})