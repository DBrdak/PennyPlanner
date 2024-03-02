import TilePaper from "../../../../components/tilesLayout/TilePaper";
import theme from "../../../theme";
import {AccountBalanceWalletTwoTone, AccountBoxTwoTone, PaymentTwoTone} from "@mui/icons-material";
import {Typography} from "@mui/material";
import {useNavigate} from "react-router-dom";

export function AccountsPreviewTile() {
    const navigate = useNavigate()

    return (
        <TilePaper onClick={() => navigate('/accounts')} sx={{
            alignItems: 'center',
            flexDirection: 'column',
            gap: theme.spacing(2),
            userSelect: 'none'
        }}>
            <AccountBalanceWalletTwoTone color={'primary'} sx={{fontSize: theme.spacing(10)}} />
            <Typography variant={'h4'}>
                Accounts
            </Typography>
        </TilePaper>
    );
}