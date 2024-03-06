import TilePaper from "../../../../components/tilesLayout/TilePaper";
import {Transaction} from "../../../../models/transactions/transaction";
import {
    Typography
} from "@mui/material";
import theme from "../../../theme";
import {TransactionEntity} from "../../../../models/transactionEntities/transactionEntity";
import {Account} from "../../../../models/accounts/account";
import {useNavigate} from "react-router-dom";
import {PaymentTwoTone} from "@mui/icons-material";

interface TransactionsPreviewTileProps {
    transactions: Transaction[]
    accounts: Account[]
    transactionEntities: TransactionEntity[]
}

export function TransactionsPreviewTile({transactions, accounts, transactionEntities}: TransactionsPreviewTileProps) {
    const navigate = useNavigate()

    // TODO Display crucial info about transactions
    return (
        <TilePaper onClick={() => navigate('/transactions')} sx={{
            alignItems: 'center',
            flexDirection: 'column',
            gap: theme.spacing(2),
            userSelect: 'none'
        }}>
            <PaymentTwoTone color={'primary'} sx={{fontSize: theme.spacing(10)}} />
            <Typography variant={'h4'}>
                Transactions
            </Typography>
        </TilePaper>
    );
}