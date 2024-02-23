import TilePaper from "../../../../components/tilesLayout/TilePaper";
import {Transaction} from "../../../../models/transactions/transaction";
import {
    Grid,
    Table,
    TableBody,
    TableCell,
    TableRow,
    Typography,
    useMediaQuery
} from "@mui/material";
import theme from "../../../theme";
import formatNumber from "../../../../utils/formatters/numberFormatter";
import formatDate from "../../../../utils/formatters/dateFormatter";
import {TransactionEntity} from "../../../../models/transactionEntities/transactionEntity";
import {Account} from "../../../../models/accounts/account";
import {useNavigate} from "react-router-dom";

interface TransactionsPreviewTileProps {
    transactions: Transaction[]
    accounts: Account[]
    transactionEntities: TransactionEntity[]
}

export function TransactionsPreviewTile({transactions, accounts, transactionEntities}: TransactionsPreviewTileProps) {
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
    const navigate = useNavigate()

    const getFrom = (transaction: Transaction) => {
        return accounts.find(a => a.accountId === transaction.fromAccountId)?.name ||
            transactionEntities.find(te => te.transactionEntityId === transaction.senderId)?.name ||
            accounts.find(a => a.accountId === transaction.accountId)?.name
    }

    const getTo = (transaction: Transaction) => {
        return accounts.find(a => a.accountId === transaction.toAccountId)?.name ||
            transactionEntities.find(te => te.transactionEntityId === transaction.recipientId)?.name ||
            accounts.find(a => a.accountId === transaction.accountId)?.name
    }

    const getFromTo = (transaction: Transaction) => {
        const from = getFrom(transaction)
        const to = getTo(transaction)

        return from === to ?
            'Adjustment' :
            `${from} ➡ ${to}`
    }

    // TODO Display crucial info about transactions and on click navigate to list with all transactions with filtering
    return (
        isMobile ?
            <TilePaper onClick={() => navigate('/transactions')} sx={{
                display:'flex',
                justifyContent: 'center',
                alignItems: 'center'
            }}>
                <Typography variant={'h3'}>Transactions</Typography>
            </TilePaper>
            :
            <TilePaper onClick={() => navigate('/transactions')} sx={{
                boxShadow: 'inset 0px 0px 10vw rgba(0, 0, 0, 1)'
            }}>
                <Typography variant={'h3'} sx={{
                    position: 'absolute',
                    left: '2vw',
                    bottom: '1.5vw',
                    userSelect: 'none'
                }}>
                    Transactions
                </Typography>
                <Grid container padding={'2% 4%'}>
                    <Grid item xs={12}>
                        <Table sx={{userSelect: 'none'}}>
                            <TableBody>
                                {transactions.slice(0, window.innerHeight / 200).map(transaction => (
                                    <TableRow key={transaction.transactionId}>
                                        <TableCell align={'center'}>
                                            {formatNumber(transaction.transactionAmount.amount)} {transaction.transactionAmount.currency}
                                        </TableCell>
                                        <TableCell align={'center'}>
                                            {transaction.category ? transaction.category.value : '-'}
                                        </TableCell>
                                        <TableCell align={'center'}>
                                            {getFromTo(transaction)}
                                        </TableCell>
                                        <TableCell align={'center'}>
                                            {formatDate(transaction.transactionDateUtc)}
                                        </TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </Grid>
                </Grid>
            </TilePaper>
    );
}