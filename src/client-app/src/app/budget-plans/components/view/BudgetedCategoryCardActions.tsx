import {Button, ButtonGroup, Grid} from "@mui/material";
import theme from "../../../theme";

interface BudgetedCategoryCardActionsProps {
    onDetailedViewEnter: () => void,
}

export function BudgetedCategoryCardActions({ onDetailedViewEnter }: BudgetedCategoryCardActionsProps) {
    return (
        <ButtonGroup fullWidth sx={{
            borderRadius: '0px 0px 2px 0px'
        }}>
            <Button onClick={() => onDetailedViewEnter()} variant={'contained'} sx={{
                borderRadius: '0px 0px 20px 20px',
                fontSize: theme.spacing(2)
            }}>
                Details
            </Button>
        </ButtonGroup>
    )
}