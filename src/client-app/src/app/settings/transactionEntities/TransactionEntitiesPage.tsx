import AppOverlay from "../../../components/appOverlay/AppOverlay";
import {CircularProgress, Grid, LinearProgress, Paper, Typography} from "@mui/material";
import theme from "../../theme";
import TilePaper from "../../../components/tilesLayout/TilePaper";
import {TilesLayout} from "../../../components/tilesLayout/TilesLayout";
import {observer} from "mobx-react-lite";
import useTransactionEntities from "../../../utils/hooks/useTransactionEntities";
import {useStore} from "../../../stores/store";
import {TransactionEntityTile} from "./components/TransactionEntityTile";
import {AddTransactionEntityTile} from "./components/AddTransactionEntityTile";
import {AddTransactionEntityCommand} from "../../../models/requests/transactionEntities/addTransactionEntityCommand";
import ConfirmModal from "../../../components/ConfirmModal";
import {useEffect, useState} from "react";
import tilePaper from "../../../components/tilesLayout/TilePaper";

export default observer (function TransactionEntitiesPage() {
    const {transactionEntityStore, modalStore} = useStore()
    const [tileLoadingIds, setTileLoadingIds] = useState<string[]>([])
    const transactionEntities = useTransactionEntities()
    const [initialLoading, setInitialLoading] = useState(true)

    useEffect(() => {
        !transactionEntityStore.loading && setInitialLoading(false)
    }, [transactionEntityStore.loading])

    const getSenders = () => transactionEntities.filter(te => te.transactionEntityType.toLowerCase() === 'sender')
    const getRecipients = () => transactionEntities.filter(te => te.transactionEntityType.toLowerCase() === 'recipient')

    const handleDelete = (transactionEntityId: string) => {
        const transactionEntityName = transactionEntities
            .find(te => te.transactionEntityId === transactionEntityId)?.name

        modalStore.openModal(<ConfirmModal
            important
            text={`You are about to delete ${transactionEntityName}. All related transactions will lose data about transaction entity. Are you sure you want to proceed?`}
            onConfirm={() => {
                setTileLoadingIds(prev => [...prev, transactionEntityId])
                transactionEntityStore.deleteTransactionEntity(transactionEntityId).then(() => {
                    setTileLoadingIds(prev => prev.filter(id => id !== transactionEntityId))
                })
            }}
        />)
    }

    const handleEdit = (transactionEntityId: string, newName: string) => {
        setTileLoadingIds(prev => [...prev, transactionEntityId])
        transactionEntityStore.updateTransactionEntity(transactionEntityId, newName).then(() => {
            setTileLoadingIds(prev => prev.filter(id => id !== transactionEntityId))
        })
    }

    function handleCreate(command: AddTransactionEntityCommand) {
        setTileLoadingIds(prev => [...prev, command.type])
        transactionEntityStore.addTransactionEntity(command).then(() => {
            setTileLoadingIds(prev => prev.filter(id => id !== command.type))
        })
    }

    return (
        <AppOverlay>
            <Grid container spacing={3} sx={{
                height:'100%',
                padding: 2,
                paddingBottom: 5,
                margin: 0,
                backgroundColor: theme.palette.background.paper,
                borderRadius: '20px',
                overflow:'auto',
                userSelect: 'none',
                maxWidth: '1920px',
            }}>
                <Paper sx={{
                    width: '100%',
                    height: theme.spacing(12),
                    display: 'flex',
                    justifyContent: 'center',
                    alignItems: 'center',
                    marginBottom: 3
                }}>
                    <Typography variant={'h3'}>
                        Senders
                    </Typography>
                </Paper>
                {
                    initialLoading ?
                        <Grid item xs={12} sx={{
                            minHeight: '200px',
                            height: '33%',
                            marginBottom: 3,
                            display: 'flex',
                            justifyContent: 'center',
                            alignItems: 'center'
                        }}>
                            <CircularProgress />
                        </Grid>
                        :
                        <>
                            {getSenders().map(transactionEntity => (
                                <TransactionEntityTile
                                    key={transactionEntity.transactionEntityId}
                                    transactionEntity={transactionEntity}
                                    onDelete={handleDelete}
                                    onEdit={handleEdit}
                                    loading={tileLoadingIds.some(id => id === transactionEntity.transactionEntityId)}
                                />
                            ))}
                            <AddTransactionEntityTile
                                onCreate={handleCreate}
                                type={'sender'}
                                loading={tileLoadingIds.some(id => id === 'sender')}
                            />
                        </>
                }
                <Paper sx={{
                    width: '100%',
                    height: theme.spacing(12),
                    display: 'flex',
                    justifyContent: 'center',
                    alignItems: 'center',
                    marginBottom: 3
                }}>
                    <Typography variant={'h3'}>
                        Recipients
                    </Typography>
                </Paper>
                {
                    initialLoading ?
                        <Grid item xs={12} sx={{
                            minHeight: '200px',
                            height: '33%',
                            marginBottom: 3,
                            display: 'flex',
                            justifyContent: 'center',
                            alignItems: 'center'
                        }}>
                            <CircularProgress/>
                        </Grid>
                        :
                        <>
                            {
                                getRecipients().map(transactionEntity => (
                                    <TransactionEntityTile
                                        key={transactionEntity.transactionEntityId}
                                        transactionEntity={transactionEntity}
                                        onDelete={handleDelete}
                                        onEdit={handleEdit}
                                        loading={tileLoadingIds.some(id => id === transactionEntity.transactionEntityId)}
                                    />
                                ))
                            }
                            <AddTransactionEntityTile
                                onCreate={handleCreate}
                                type={'recipient'}
                                loading={tileLoadingIds.some(id => id === 'recipient')}
                            />
                        </>
                }
            </Grid>
        </AppOverlay>
    );
})