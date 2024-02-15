import TilePaper from "../../../../components/tilesLayout/TilePaper";
import {EventRepeatTwoTone} from "@mui/icons-material";
import theme from "../../../theme";
import {Typography} from "@mui/material";
import {useNavigate} from "react-router-dom";

export function RecurringTransactionsTile() {
    const navigate = useNavigate()

    return (
        <TilePaper onClick={() => navigate('/settings/recurring-transactions')} sx={{
            alignItems: 'center',
            flexDirection: 'column',
            gap: theme.spacing(2)
        }}>
            <EventRepeatTwoTone color={'primary'} sx={{fontSize: theme.spacing(10)}} />
            <Typography variant={'h4'}>
                Recurring Transactions
            </Typography>
        </TilePaper>
    );
}