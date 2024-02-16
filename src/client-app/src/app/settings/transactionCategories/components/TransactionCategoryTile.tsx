import {Box, CircularProgress, Grid, IconButton, Typography} from "@mui/material";
import TilePaper from "../../../../components/tilesLayout/TilePaper";
import {useState} from "react";
import {Delete, DeleteTwoTone, EditTwoTone} from "@mui/icons-material";
import theme from "../../../theme";
import {Formik} from "formik";
import {UpdateTransactionCategoryForm} from "./UpdateTransactionCategoryForm";
import {TransactionCategory} from "../../../../models/transactionCategories/transactionCategory";

interface TransactionCategoryTileProps {
    transactionCategory: TransactionCategory
    onDelete: (transactionCategoryId: string) => void
    onEdit: (transactionCategoryId: string, newValue: string) => void
    loading: boolean
}

export function TransactionCategoryTile({transactionCategory, onDelete, onEdit, loading}: TransactionCategoryTileProps) {
    const [editMode, setEditMode] = useState(false)

    return (
        <Grid item xs={12} md={6} lg={3} sx={{
            minHeight: '200px',
            height: '33%',
            marginBottom: 3}
        }>
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
                            <UpdateTransactionCategoryForm
                                transactionCategory={transactionCategory}
                                onDelete={onDelete}
                                onExit={() => setEditMode(false)}
                                onSubmit={onEdit}
                            />
                            :
                            <>
                                <Typography variant={'h3'}>
                                    {transactionCategory.value}
                                </Typography>
                                <Typography variant={'subtitle1'} color={
                                    transactionCategory.type.toLowerCase() === 'income' ? 'primary' : 'secondary'
                                }>
                                    {transactionCategory.type}
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
                                    <IconButton color={'error'} sx={{width: '50%', borderRadius: 0}} onClick={() => {
                                        onDelete(transactionCategory.transactionCategoryId)
                                    }}>
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