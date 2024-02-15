import {Box, Button, Grid, IconButton, Typography} from "@mui/material";
import TilePaper from "../../../../components/tilesLayout/TilePaper";
import theme from "../../../theme";
import {AddTwoTone, DeleteTwoTone, EditTwoTone} from "@mui/icons-material";
import {useState} from "react";
import {AddTransactionEntityCommand} from "../../../../models/requests/addTransactionEntityCommand";

interface AddTransactionEntityTileProps {
    onCreate: (command: AddTransactionEntityCommand) => void
}

export function AddTransactionEntityTile({onCreate}: AddTransactionEntityTileProps) {
    const [createMode, setCreateMode] = useState(false)

    return (
        <Grid item xs={12} md={6} lg={3} sx={{minHeight: '200px', height: '33%'}}>
            <TilePaper onClick={() => setCreateMode(true)} sx={{
                alignItems: 'center',
                justifyContent: 'center',
                flexDirection: 'column',
                userSelect: 'none'
            }}>
                { // TODO Set loading on particular add tile
                    createMode ?
                        <>
                        </> :
                        <AddTwoTone />
                }
            </TilePaper>
        </Grid>
    );
}