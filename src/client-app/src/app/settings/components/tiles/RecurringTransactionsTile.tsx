import TilePaper from "../../../../components/tilesLayout/TilePaper";
import {EventRepeatTwoTone} from "@mui/icons-material";
import theme from "../../../theme";
import {Typography} from "@mui/material";
import {useNavigate} from "react-router-dom";

// TODO Implement Feature
export function RecurringTransactionsTile() {
    const navigate = useNavigate()

    return (
        <TilePaper disabled /*onClick={() => navigate('/settings/recurring-transactions')}*/ sx={{
            alignItems: 'center',
            flexDirection: 'column',
            gap: theme.spacing(2),
            userSelect: 'none',
            textAlign: 'center'
        }}>
            <EventRepeatTwoTone color={'primary'} sx={{fontSize: theme.spacing(10)}} />
            <Typography variant={'h4'}>
                Recurring Transactions
            </Typography>
            <Typography variant={'subtitle1'} color={'palegoldenrod'}>
                Available soon!
            </Typography>
        </TilePaper>
    );
}