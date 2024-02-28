import {Box, Button, ButtonGroup, Stack} from "@mui/material";
import theme from "../../../theme";

interface BudgetPlanEditButtonParams {
    setEditMode: (state: boolean) => void
    editMode: boolean
}

export function BudgetPlanEditButton({setEditMode, editMode}: BudgetPlanEditButtonParams) {
    return (
        <Box sx={{
            width: '100%',
            position: 'sticky',
            height: '50px',
            bottom: 15,
            display: 'flex',
            justifyContent: 'center',
            alignItems:'center',
        }}>
            {
                editMode ?
                    null
                    :
                    <Button
                        variant={'outlined'} color={'secondary'}
                        onClick={() => setEditMode(true)}
                        sx={{
                            position: 'absolute',
                            bottom: 0,
                            width: '200px',
                            height: '50px',
                            zIndex: 101
                        }}>
                        Edit Budget
                    </Button>
            }
        </Box>
    )
}