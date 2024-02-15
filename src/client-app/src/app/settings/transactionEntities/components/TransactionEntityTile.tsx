import {TransactionEntity} from "../../../../models/transactionEntities/transactionEntity";
import {Box, Grid, IconButton, Typography} from "@mui/material";
import TilePaper from "../../../../components/tilesLayout/TilePaper";
import {useState} from "react";
import {Delete, DeleteTwoTone, EditTwoTone} from "@mui/icons-material";
import theme from "../../../theme";

interface TransactionEntityTileProps {
    transactionEntity: TransactionEntity
    onDelete: (transactionEntityId: string) => void
    onEdit: (transactionEntityId: string) => void
}

export function TransactionEntityTile({transactionEntity, onDelete, onEdit}: TransactionEntityTileProps) {
    const [editMode, setEditMode] = useState(false)

    function handleDelete() {

    }

    return (
        <Grid item xs={12} lg={4} sx={{minHeight: '200px', height: '33%'}}>
            <TilePaper disabled sx={{
                alignItems: 'center',
                justifyContent: 'center',
                flexDirection: 'column',
                userSelect: 'none',
                position: 'relative'
            }}>
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
                    <IconButton sx={{width: '50%', borderRadius: 0}} onClick={() => handleDelete()}>
                        <DeleteTwoTone color={'error'} fontSize={'large'} />
                    </IconButton>
                    <IconButton sx={{width: '50%', borderRadius: 0}} onClick={() => setEditMode(true)}>
                        <EditTwoTone color={'inherit'} fontSize={'large'} />
                    </IconButton>
                </Box>
            </TilePaper>
        </Grid>
    );
}