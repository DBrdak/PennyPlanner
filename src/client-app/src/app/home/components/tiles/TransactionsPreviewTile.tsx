import TilePaper from "../../../../components/tilesLayout/TilePaper";
import {Transaction} from "../../../../models/transactions/transaction";
import {
    Grid,
    LinearProgress,
    List,
    ListItem,
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
import {ArrowRight} from "@mui/icons-material";
import {TransactionEntity} from "../../../../models/transactionEntities/transactionEntity";
import {Account} from "../../../../models/accounts/account";
import {useEffect, useState} from "react";

interface TransactionsPreviewTileProps {
    transactions: Transaction[]
    accounts: Account[]
    transactionEntities: TransactionEntity[]
}

export function TransactionsPreviewTile({transactions, accounts, transactionEntities}: TransactionsPreviewTileProps) {
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
    const [transactionsPage, setTransactionsPage] = useState<Transaction[]>(transactions.slice(0, 9))
    const [currentPageIndex, setCurrentPageIndex] = useState(0)

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

    useEffect(() => {
        const interval = setInterval(() => {
            const nextPageIndex = (currentPageIndex + 1) % Math.ceil(transactions.length / 9);

            const startIndex = nextPageIndex * 9;
            const endIndex = Math.min(startIndex + 8, transactions.length - 1);

            setTransactionsPage(transactions.slice(startIndex, endIndex + 1));

            setCurrentPageIndex(nextPageIndex);
        }, 5000);

        return () => clearInterval(interval);
    }, [currentPageIndex, transactions])

    // TODO Display crucial info about transactions and on click navigate to list with all transactions with filtering
    return (
        isMobile ?
            <TilePaper sx={{display:'flex', justifyContent: 'center', alignItems: 'center'}}>
                <Typography variant={'h3'}>Transactions</Typography>
            </TilePaper>
            :
            <TilePaper sx={{boxShadow: 'inset 0px 0px 10vw rgba(0, 0, 0, 1)'}}>
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
                                {transactionsPage.map(transaction => (
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