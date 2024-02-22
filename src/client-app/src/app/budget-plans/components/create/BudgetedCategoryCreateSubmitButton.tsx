import {Box, Button} from "@mui/material";

export function BudgetedCategoryCreateSubmitButton() {
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
            <Button sx={{height: '100%', width: '30%'}} variant={'contained'} color={'success'}>
                Submit
            </Button>
        </Box>
    );
}