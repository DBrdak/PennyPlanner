import {AddInternalTransactionCommand} from "../../../../models/requests/transactions/addInternalTransactionCommand";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../../stores/store";
import {Paper, Table, TableBody, TableCell, TableHead, TableRow, Typography} from "@mui/material";
import formatNumber from "../../../../utils/formatters/numberFormatter";
import formatDate from "../../../../utils/formatters/dateFormatter";

interface NewInternalTransactionsTableProps {
    newTransactions: AddInternalTransactionCommand[]
}

export default observer (function NewInternalTransactionsTable({newTransactions}: NewInternalTransactionsTableProps) {
    const {accountStore} = useStore()
    return (
        <Paper>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell colSpan={5}>
                            <Typography variant={'h6'} sx={{width: '100%', textAlign: 'center'}}>
                                New internal transactions
                            </Typography>
                        </TableCell>
                    </TableRow>
                    <TableRow>
                        <TableCell align={'center'}>
                            <strong>From Account</strong>
                        </TableCell>
                        <TableCell align={'center'}>
                            <strong>To Account</strong>
                        </TableCell>
                        <TableCell align={'center'}>
                            <strong>Amount</strong>
                        </TableCell>
                        <TableCell align={'center'}>
                            <strong>Date</strong>
                        </TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {newTransactions.map((transaction, index) => (
                        <TableRow key={index}>
                            <TableCell align={'center'}>
                                {accountStore.getAccount(transaction.fromAccountId)?.name || ''}
                            </TableCell>
                            <TableCell align={'center'}>
                                {accountStore.getAccount(transaction.toAccountId)?.name || ''}
                            </TableCell>
                            <TableCell align={'center'}>
                                {formatNumber(transaction.transactionAmount)} {'USD' /*TODO Retrieve user currency*/}
                            </TableCell>
                            <TableCell align={'center'}>
                                {formatDate(transaction.transactionDateTime)}
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </Paper>
    )
})