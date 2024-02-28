import {Box, Button, ButtonGroup, Stack} from "@mui/material";
import theme from "../../../theme";

interface BudgetPlanEditButtonParams {
    setEditMode: (value: (((prevState: boolean) => boolean) | boolean)) => void
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
                    <Stack direction={'row'}
                           alignItems={'center'}
                           justifyContent={'center'}
                           spacing={theme.spacing(3)}
                           sx={{height: '100%', width: '100%'}}>
                        <Button
                            onClick={() => setEditMode(false)}
                            color={'inherit'}
                            variant={'contained'}
                            sx={{height: '100%', minWidth: '20%'}}
                        >
                            Exit Edit Mode
                        </Button>
                        <Button
                            variant={'contained'}
                            color={'error'}
                            sx={{height: '100%', minWidth: '20%'}}
                        >
                            Delete Budget Plan
                        </Button>
                        <Button
                            variant={'contained'}
                            color={'success'}
                            sx={{height: '100%', minWidth: '20%'}}
                        >
                            Submit
                        </Button>
                    </Stack>
                    :
                    <Button
                        onClick={() => setEditMode(true)}
                        sx={{height: '100%', width: '30%'}}
                        variant={'contained'}
                        color={'inherit'}
                    >
                        Edit Budget Plan
                    </Button>
            }
        </Box>
    )
}