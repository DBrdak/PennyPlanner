import {CircularProgress, Grid} from "@mui/material";
import TilePaper from "../../../../components/tilesLayout/TilePaper";
import {AddTwoTone} from "@mui/icons-material";
import {useState} from "react";
import {AddTransactionEntityCommand} from "../../../../models/requests/transactionEntities/addTransactionEntityCommand";
import {AddTransactionEntityForm} from "./AddTransactionEntityForm";

interface AddTransactionEntityTileProps {
    onCreate: (command: AddTransactionEntityCommand) => void
    type: 'sender' | 'recipient'
    loading: boolean
}

export function AddTransactionEntityTile({onCreate, type, loading}: AddTransactionEntityTileProps) {
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
                            <AddTransactionEntityForm
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