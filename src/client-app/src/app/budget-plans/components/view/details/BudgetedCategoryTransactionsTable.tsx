import {Transaction} from "../../../../../models/transactions/transaction";
import {Table, TableBody, TableCell, TableContainer, TableHead, TableRow, useMediaQuery} from "@mui/material";
import theme from "../../../../theme";
import formatDate from "../../../../../utils/formatters/dateFormatter";
import formatMoney from "../../../../../utils/formatters/moneyFormatter";

interface BudgetedCategoryTransactionsTableParams {
    transactions: Transaction[]
}

export function BudgetedCategoryTransactionsTable({transactions}: BudgetedCategoryTransactionsTableParams) {

    return (
        <TableContainer>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell align={'center'}>
                            Subcategory
                        </TableCell>
                        <TableCell align={'center'}>
                            Date
                        </TableCell>
                        <TableCell align={'center'}>
                            Amount
                        </TableCell>
                    </TableRow>
                </TableHead>
                <TableBody sx={{maxHeight: '25vh', overflow: 'auto'}}>
                    {transactions.map(transaction => (
                        <TableRow key={transaction.transactionId}>
                            <TableCell align={'center'}>
                                {transaction.subcategory?.value}
                            </TableCell>
                            <TableCell align={'center'}>
                                {formatDate(transaction.transactionDateUtc)}
                            </TableCell>
                            <TableCell align={'center'}>
                                {formatMoney(transaction.transactionAmount)}
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    )
}