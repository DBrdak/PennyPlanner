import {Paper, Table, TableBody, TableCell, TableHead, TableRow, Typography} from "@mui/material";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../../stores/store";
import {AddIncomeTransactionCommand} from "../../../../models/requests/transactions/addIncomeTransactionCommand";
import formatNumber from "../../../../utils/formatters/numberFormatter";
import formatDate from "../../../../utils/formatters/dateFormatter";

interface NewIncomesTableParams {
    newIncomes: AddIncomeTransactionCommand[]
}

export default observer (function NewIncomesTable({newIncomes}: NewIncomesTableParams) {
    const {accountStore} = useStore()

    return (
        <Paper>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell colSpan={5}>
                            <Typography variant={'h6'} sx={{width: '100%', textAlign: 'center'}}>
                                New income
                            </Typography>
                        </TableCell>
                    </TableRow>
                    <TableRow>
                        <TableCell align={'center'}>
                            <strong>Account</strong>
                        </TableCell>
                        <TableCell align={'center'}>
                            <strong>Sender</strong>
                        </TableCell>
                        <TableCell align={'center'}>
                            <strong>Category</strong>
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
                    {newIncomes.map((transaction, index) => (
                        <TableRow key={index}>
                            <TableCell align={'center'}>
                                {accountStore.getAccount(transaction.destinationAccountId)?.name || ''}
                            </TableCell>
                            <TableCell align={'center'}>
                                {transaction.senderName}
                            </TableCell>
                            <TableCell align={'center'}>
                                {transaction.categoryValue}
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