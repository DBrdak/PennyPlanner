import {CircularProgress, Grid} from "@mui/material";
import TilePaper from "../../../../components/tilesLayout/TilePaper";
import {AddTwoTone} from "@mui/icons-material";
import {useState} from "react";
import {AddTransactionCategoryCommand} from "../../../../models/requests/categories/addTransactionCategoryCommand";
import {AddTransactionCategoryForm} from "./AddTransactionCategoryForm";
import {
    AddTransactionSubcategoryCommand
} from "../../../../models/requests/subcategories/addTransactionSubcategoryCommand";

interface AddTransactionCategoryTileProps {
    onCreate: (command: AddTransactionCategoryCommand | AddTransactionSubcategoryCommand) => void
    type: 'income' | 'outcome'
    loading: boolean
    categoryId?: string
}

export function AddTransactionCategoryTile({onCreate, type, loading, categoryId}: AddTransactionCategoryTileProps) {
    const [createMode, setCreateMode] = useState(false)

    return (
        <Grid item xs={12} md={4} lg={3} sx={{
            minHeight: '300px',
            height: '33%',
            marginBottom: 3,
            display: 'flex'
        }}>
            <TilePaper disabled={createMode} onClick={() => !createMode && setCreateMode(true)} sx={{
                padding: 1.5,
                width: '100%',
                justifyContent: 'center',
                alignItems: 'center'
            }}>
                {
                    loading ?
                        <CircularProgress />
                        :
                        createMode ?
                            <AddTransactionCategoryForm
                                categoryId={categoryId}
                                type={type}
                                onSubmit={(command) => {
                                    setCreateMode(false)
                                    onCreate(command)
                                }}
                                onExit={() => setCreateMode(false)}
                            /> :
                            <AddTwoTone fontSize={'large'} />
                }
            </TilePaper>
        </Grid>
    );
}