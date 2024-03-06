import TilePaper from "../../../../components/tilesLayout/TilePaper";
import {PeopleAltTwoTone} from "@mui/icons-material";
import theme from "../../../theme";
import {Typography} from "@mui/material";
import {useNavigate} from "react-router-dom";

export function TransactionEntitiesTile() {
    const navigate = useNavigate()

    return (
        <TilePaper onClick={() => navigate('/settings/transaction-entities')} sx={{
            alignItems: 'center',
            flexDirection: 'column',
            gap: theme.spacing(2),
            userSelect: 'none',
            textAlign: 'center'
        }}>
            <PeopleAltTwoTone color={'primary'} sx={{fontSize: theme.spacing(10)}} />
            <Typography variant={'h4'}>
                Transaction Entities
            </Typography>
        </TilePaper>
    );
}