import {TransactionEntity} from "../../../../models/transactionEntities/transactionEntity";
import {Box, CircularProgress, Grid, IconButton, Typography} from "@mui/material";
import TilePaper from "../../../../components/tilesLayout/TilePaper";
import {useState} from "react";
import {Delete, DeleteTwoTone, EditTwoTone} from "@mui/icons-material";
import theme from "../../../theme";
import {Formik} from "formik";
import {UpdateTransactionEntityForm} from "./UpdateTransactionEntityForm";
import {runInAction} from "mobx";

interface TransactionEntityTileProps {
    transactionEntity: TransactionEntity
    onDelete: (transactionEntityId: string) => void
    onEdit: (transactionEntityId: string, newName: string) => void
    loading: boolean
}

export function TransactionEntityTile({transactionEntity, onDelete, onEdit, loading}: TransactionEntityTileProps) {
    const [editMode, setEditMode] = useState(false)

    return (
        <Grid item xs={12} md={6} lg={3} sx={{
            minHeight: '200px',
            height: '33%',
            marginBottom: 3,
            transition: 'opacity 0.5s ease',
        }}>
            <TilePaper disabled sx={{
                alignItems: 'center',
                justifyContent: 'center',
                flexDirection: 'column',
                position: 'relative'
            }}>
                {
                    loading ?
                        <CircularProgress />
                        :
                        editMode ?
                            <UpdateTransactionEntityForm
                                transactionEntity={transactionEntity}
                                onDelete={onDelete}
                                onExit={() => setEditMode(false)}
                                onSubmit={onEdit}
                            />
                            :
                            <>
                                <Typography variant={'h3'}>
                                    {transactionEntity.name}
                                </Typography>
                                <Typography variant={'subtitle1'} color={
                                    transactionEntity.transactionEntityType.toLowerCase() === 'sender' ? 'primary' : 'secondary'
                                }>
                                    {transactionEntity.transactionEntityType}
                                </Typography>
                                <Box sx={{
                                    position: 'absolute',
                                    padding: theme.spacing(2),
                                    bottom: 0, left: 0, right: 0,
                                    height: '20%',
                                    display: 'flex',
                                    justifyContent: 'center',
                                    alignItems: 'center',
                                }}>
                                    <IconButton color={'error'} sx={{width: '50%', borderRadius: 0}} onClick={() => onDelete(transactionEntity.transactionEntityId)}>
                                        <DeleteTwoTone fontSize={'large'} />
                                    </IconButton>
                                    <IconButton color={'inherit'} sx={{width: '50%', borderRadius: 0}} onClick={() => setEditMode(true)}>
                                        <EditTwoTone fontSize={'large'} />
                                    </IconButton>
                                </Box>
                            </>
                }
            </TilePaper>
        </Grid>
    );
}