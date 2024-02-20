import { ButtonGroup, CircularProgress, Grid, IconButton, Typography} from "@mui/material";
import TilePaper from "../../../../components/tilesLayout/TilePaper";
import React, {useState} from "react";
import {DeleteTwoTone, EditTwoTone, KeyboardArrowDown, KeyboardArrowUp} from "@mui/icons-material";
import {UpdateTransactionCategoryForm} from "./UpdateTransactionCategoryForm";
import {TransactionCategory} from "../../../../models/transactionCategories/transactionCategory";
import {observer} from "mobx-react-lite";
import useSelectedCategory from "../../../../utils/hooks/useSelectedCategory";
import {useStore} from "../../../../stores/store";

interface TransactionCategoryTileProps {
    transactionCategory: TransactionCategory
    onDelete: (transactionCategoryId: string) => void
    onEdit: (transactionCategoryId: string, newValue: string) => void
    loading: boolean
}

export default observer (function TransactionCategoryTile({transactionCategory, onDelete, onEdit, loading}: TransactionCategoryTileProps) {
    const [editMode, setEditMode] = useState(false)
    const [subcategoriesVisible, setSubcategoriesVisible] = useState(false)
    const {categoryStore} = useStore()
    const selectedCategory = useSelectedCategory()

    function handleSubcategoriesClick(transactionCategoryId: string) {
        categoryStore.selectedCategory?.transactionCategoryId === transactionCategoryId
            ? categoryStore.setSelectedCategory(undefined)
            : categoryStore.setSelectedCategory(transactionCategoryId)
    }

    return (
        <Grid item xs={12} md={4} lg={3} alignItems="center" justifyContent="center"  sx={{
            minHeight: '200px',
            height: '33%',
            marginBottom: 3,
            display: 'flex'
        }}>
            <TilePaper disabled sx={{
                padding: 1.5,
                maxWidth: '1200px',
                width: '100%',
                justifyContent: 'center',
                alignItems: 'center'
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
                            <Grid container height={'100%'}>
                                <Grid item xs={12} sx={{
                                    height: '70%',
                                    display: 'flex',
                                    justifyContent: 'center',
                                    alignItems: 'center',
                                    flexDirection: 'column'
                                }}>
                                    <Typography variant={'h3'} width={'90%'} textAlign={'center'} sx={{ overflowWrap: 'break-word', overflowY: 'hidden' }}>
                                        {transactionCategory.value}
                                    </Typography>
                                    <Typography variant={'subtitle1'} color={
                                        transactionCategory.type.toLowerCase() === 'income' ? 'primary' : 'secondary'
                                    }>
                                        {transactionCategory.type}
                                    </Typography>
                                </Grid>
                                <Grid item xs={12} sx={{
                                    height: '30%',
                                    display: 'flex',
                                    justifyContent: 'center',
                                    alignItems: 'center',
                                    flexDirection: 'column'
                                }}>
                                    <ButtonGroup fullWidth>
                                        <IconButton color={'error'} sx={{width: '50%', borderRadius: 0}} onClick={() => {
                                            onDelete(transactionCategory.transactionCategoryId)
                                        }}>
                                            <DeleteTwoTone fontSize={'large'} />
                                        </IconButton>
                                        <IconButton color={'inherit'} sx={{width: '50%', borderRadius: 0}} onClick={() => setEditMode(true)}>
                                            <EditTwoTone fontSize={'large'} />
                                        </IconButton>
                                    </ButtonGroup>
                                    <IconButton
                                        onClick={() => handleSubcategoriesClick(transactionCategory.transactionCategoryId)}
                                        sx={{
                                            width: '100%',
                                            borderRadius: 0,
                                            flexDirection: 'column'
                                    }}>
                                        <Typography variant={'caption'}>
                                            Subcategories
                                        </Typography>
                                        {
                                            selectedCategory?.transactionCategoryId === transactionCategory.transactionCategoryId ?
                                                <KeyboardArrowUp/> :
                                                <KeyboardArrowDown/>
                                        }
                                    </IconButton>
                                </Grid>
                            </Grid>
                }
            </TilePaper>
        </Grid>
    );
})