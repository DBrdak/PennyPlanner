import TilePaper from "../../../../components/tilesLayout/TilePaper";
import {FolderCopyTwoTone} from "@mui/icons-material";
import {Typography} from "@mui/material";
import theme from "../../../theme";
import {useNavigate} from "react-router-dom";

export function TransactionCategoriesTile() {
    const navigate = useNavigate()

    return (
        <TilePaper onClick={() => navigate('/settings/transaction-categories')} sx={{
            alignItems: 'center',
            flexDirection: 'column',
            gap: theme.spacing(2),
            userSelect: 'none',
            textAlign: 'center'
        }}>
            <FolderCopyTwoTone color={'primary'} sx={{fontSize: theme.spacing(10)}} />
            <Typography variant={'h4'}>
                Transaction Categories
            </Typography>
        </TilePaper>
    );
}