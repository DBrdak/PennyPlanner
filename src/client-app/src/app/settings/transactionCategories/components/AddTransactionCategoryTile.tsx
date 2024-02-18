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
    onCreate: (command: AddTransactionCategoryCommand) => void
    type: 'income' | 'outcome'
    loading: boolean
}

export function AddTransactionCategoryTile({onCreate, type, loading}: AddTransactionCategoryTileProps) {
    const [createMode, setCreateMode] = useState(false)

    return (
        <Grid item xs={12} md={6} lg={3} sx={{
            minHeight: '200px',
            height: '33%',
            marginBottom: 3
        }}>
            <TilePaper disabled={createMode} onClick={() => !createMode && setCreateMode(true)} sx={{
                alignItems: 'center',
                justifyContent: 'center',
                flexDirection: 'column',
                userSelect: 'none'
            }}>
                {
                    loading ?
                        <CircularProgress />
                        :
                        createMode ?
                            <AddTransactionCategoryForm
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