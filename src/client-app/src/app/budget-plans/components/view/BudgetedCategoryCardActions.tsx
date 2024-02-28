import {Button, ButtonGroup, Grid} from "@mui/material";
import theme from "../../../theme";

interface BudgetedCategoryCardActionsProps {
    onEditModeEnter: () => void
    onTransactionsViewEnter: () => void
}

export function BudgetedCategoryCardActions({onEditModeEnter, onTransactionsViewEnter}: BudgetedCategoryCardActionsProps) {
    return (
        <ButtonGroup fullWidth sx={{
            borderRadius: '0px 0px 2px 0px'
        }}>
            <Button variant={'contained'} sx={{
                borderRadius: '0px 0px 0px 20px',
                fontSize: theme.spacing(2)
            }}>
                View Transactions
            </Button>
            <Button sx={{
                borderRadius: '0px 0px 20px 0px'
            }}>
                Edit
            </Button>
        </ButtonGroup>
    )
}