import TilePaper from "../../../../components/tilesLayout/TilePaper";
import {useNavigate} from "react-router-dom";
import theme from "../../../theme";
import {CalendarMonthTwoTone, PaymentTwoTone} from "@mui/icons-material";
import {Typography} from "@mui/material";

export function BudgetPreviewTile() {
    const navigate = useNavigate()

    // TODO Display budget preview and on click navigate to budget plan for current month
    return (
        <TilePaper onClick={() => navigate('/budget-plans')} sx={{
            alignItems: 'center',
            flexDirection: 'column',
            gap: theme.spacing(2),
            userSelect: 'none'
        }}>
            <CalendarMonthTwoTone color={'primary'} sx={{fontSize: theme.spacing(10)}} />
            <Typography variant={'h4'}>
                Budget plans
            </Typography>
        </TilePaper>
    );
}