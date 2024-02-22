import {Box, Button} from "@mui/material";

interface BudgetedCategoryCreateSubmitButtonProps{
    onSubmit: () => void
    disabled: boolean
}

export function BudgetedCategoryCreateSubmitButton({onSubmit, disabled}: BudgetedCategoryCreateSubmitButtonProps) {
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
            <Button
                disabled={disabled}
                onClick={() => onSubmit()}
                sx={{height: '100%', width: '30%'}}
                variant={'contained'}
                color={'success'}>
                Submit
            </Button>
        </Box>
    );
}