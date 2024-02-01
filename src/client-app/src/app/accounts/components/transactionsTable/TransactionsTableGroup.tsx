import {Collapse, Table, TableBody, TableCell, TableContainer, TableHead, TableRow} from "@mui/material";
import SortableTableCell from "./SortableTableCell";
import {useState} from "react";
import {Transaction} from "../../../../models/transactions/transaction";
import formatDate from "../../../../utils/dateFormatter";

interface TransactionsTableGroupProps {
    groupedTransactions: Record<string, Transaction[]>
    groupCriterion: string
    groupKey: string
}

export function TransactionsTableGroup({groupedTransactions, groupCriterion, groupKey}: TransactionsTableGroupProps) {
    const [sortBy, setSortBy] = useState<string | null>(null);
    const [sortOrder, setSortOrder] = useState<'asc' | 'desc'>('asc');


    const handleSort = (column: string) => {
        if (column === sortBy) {
            setSortOrder(sortOrder === 'asc' ? 'desc' : 'asc');
        } else {
            setSortBy(column);
            setSortOrder('asc');
        }
    };

    const sortTransactionGroup = (group: Transaction[]): Transaction[] => {
        return [...group].sort((a, b) => {
            switch (sortBy) {
                case 'amount':
                    return sortOrder === 'asc' ? a.transactionAmount.amount - b.transactionAmount.amount : b.transactionAmount.amount - a.transactionAmount.amount;
                case 'date':
                    return sortOrder === 'asc' ? new Date(a.transactionDateUtc).getTime() - new Date(b.transactionDateUtc).getTime() : new Date(b.transactionDateUtc).getTime() - new Date(a.transactionDateUtc).getTime();
                default:
                    return 0;
            }
        });
    };

    return (
        <TableContainer sx={{overflow: 'auto'}}>
            <Table>
                <TableHead sx={{backgroundColor: '#121212'}}>
                    <TableRow>
                        <TableCell />
                        <SortableTableCell
                            label="Amount"
                            column="amount"
                            sortOrder={sortOrder}
                            sortBy={sortBy}
                            onSort={handleSort}
                        />
                        {
                            groupCriterion === 'category' ||
                            <TableCell align={'center'}>
                                Category
                            </TableCell>
                        }
                        {
                            groupCriterion === 'sender/recipient' ||
                            <TableCell align={'center'}>
                                Sender / Recipient
                            </TableCell>
                        }
                        <SortableTableCell
                            label="Transaction Date"
                            column="date"
                            sortOrder={sortOrder}
                            sortBy={sortBy}
                            onSort={handleSort}
                        />
                    </TableRow>
                </TableHead>
                <TableBody>
                    {sortTransactionGroup(groupedTransactions[groupKey]).map((transaction) => (
                        <TableRow key={transaction.transactionId}>
                            <TableCell />
                            <TableCell align={'center'}>
                                {transaction.transactionAmount.amount} {transaction.transactionAmount.currency}
                            </TableCell>
                            {groupCriterion === 'category' || <TableCell align={'center'}>{transaction.category}</TableCell>}
                            {
                                groupCriterion === 'sender/recipient' ||
                                <TableCell align={'center'}>
                                    {
                                        transaction.fromAccountId ||
                                        transaction.toAccountId ||
                                        transaction.senderId ||
                                        transaction.recipientId ||
                                        '-'
                                    }
                                </TableCell>
                            }
                            <TableCell align={'center'}>{formatDate(transaction.transactionDateUtc)}</TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </TableContainer>
    );
}