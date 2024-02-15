import AppOverlay from "../../../components/appOverlay/AppOverlay";
import {CircularProgress, Grid, Paper, Typography} from "@mui/material";
import theme from "../../theme";
import TilePaper from "../../../components/tilesLayout/TilePaper";
import {TilesLayout} from "../../../components/tilesLayout/TilesLayout";
import {observer} from "mobx-react-lite";
import useTransactionEntities from "../../../utils/hooks/useTransactionEntities";
import {useStore} from "../../../stores/store";
import {TransactionEntityTile} from "./components/TransactionEntityTile";
import {AddTransactionEntityTile} from "./components/AddTransactionEntityTile";
import {AddTransactionEntityCommand} from "../../../models/requests/addTransactionEntityCommand";

export default observer (function TransactionEntitiesPage() {
    const {transactionEntityStore} = useStore()
    const transactionEntities = useTransactionEntities()

    const getSenders = () => transactionEntities.filter(te => te.transactionEntityType.toLowerCase() === 'sender')
    const getRecipients = () => transactionEntities.filter(te => te.transactionEntityType.toLowerCase() === 'recipient')

    const handleDelete = (transactionEntityId: string) => {

    }

    const handleEdit = (transactionEntityId: string) => {

    }

    function handleCreate(command: AddTransactionEntityCommand) {

    }

    return (
        <AppOverlay>
            <Grid container spacing={3} sx={{
                height:'100%',
                padding: 2,
                margin: 0,
                backgroundColor: theme.palette.background.paper,
                borderRadius: '20px',
                overflow:'auto',
                userSelect: 'none'
            }}>
                {
                    transactionEntityStore.loading ?
                        <Grid item xs={12} height={'100%'} sx={{display: 'flex', alignItems: 'center', justifyContent: 'center'}}>
                            <CircularProgress />
                        </Grid> :
                        <>
                            <Paper sx={{
                                width: '100%',
                                display: 'flex',
                                justifyContent: 'center',
                                alignItems: 'center'
                            }}>
                                <Typography variant={'h3'}>
                                    Senders
                                </Typography>
                            </Paper>
                            {
                                getSenders().map(transactionEntity => (
                                    <TransactionEntityTile
                                        key={transactionEntity.transactionEntityId}
                                        transactionEntity={transactionEntity}
                                        onDelete={handleDelete}
                                        onEdit={handleEdit}
                                    />
                                ))
                            }
                            <AddTransactionEntityTile onCreate={handleCreate} />
                            <Paper sx={{
                                width: '100%',
                                display: 'flex',
                                justifyContent: 'center',
                                alignItems: 'center'
                            }}>
                                <Typography variant={'h3'}>
                                    Recipients
                                </Typography>
                            </Paper>
                            {
                                getRecipients().map(transactionEntity => (
                                    <TransactionEntityTile
                                        key={transactionEntity.transactionEntityId}
                                        transactionEntity={transactionEntity}
                                        onDelete={handleDelete}
                                        onEdit={handleEdit}
                                    />
                                ))
                            }
                            <AddTransactionEntityTile onCreate={handleCreate} />
                        </>
                }
            </Grid>
        </AppOverlay>
    );
})