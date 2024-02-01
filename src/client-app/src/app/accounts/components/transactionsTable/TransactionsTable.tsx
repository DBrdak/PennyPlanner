import {Table, TableBody, TableCell, TableHead, TableRow} from "@mui/material";
import {Transaction} from "../../../../models/transactions/transaction";
import {Fragment, useState} from "react";
import theme from "../../../theme";
import SortableTableCell from "./SortableTableCell";

interface TransactionsTableProps {
    transactions: Transaction[];
    groupCriterion: string
}


export function TransactionsTable({transactions, groupCriterion}: TransactionsTableProps) {
    const [sortBy, setSortBy] = useState<string | null>(null);
    const [sortOrder, setSortOrder] = useState<'asc' | 'desc'>('asc');

    const formatDate = (dateString: Date) => {
        const date = new Date(dateString)
        const options: Intl.DateTimeFormatOptions = {
            day: '2-digit',
            month: '2-digit',
            year: 'numeric',
            hour: '2-digit',
            minute: '2-digit',
        };

        return date.toLocaleString('pl-PL', options);
    };

    const groupBy = (transactions: Transaction[], criterion: string): Record<string, Transaction[]> => {
        const groupedTransactions: Record<string, Transaction[]> = {};

        transactions.forEach((transaction) => {
            let key

            switch (criterion) {
                case 'day':
                    key = formatDate(transaction.transactionDateUtc).slice(0, 10)
                    break
                case 'month':
                    key = formatDate(transaction.transactionDateUtc).slice(3, 10)
                    break
                case 'year':
                    key = formatDate(transaction.transactionDateUtc).slice(6, 10)
                    break
                case 'sender/recipient':
                    criterion = 'recipientId' || 'senderId' || 'fromAccountId' || 'toAccountId'
                    key = transaction[criterion as keyof Transaction] as string || 'Private'
                    break
                default:
                    key = transaction[criterion as keyof Transaction] as string || 'Unknown'
                    break
            }

            if (!groupedTransactions[key]) {
                groupedTransactions[key] = [];
            }

            groupedTransactions[key].push(transaction);
        });

        return groupedTransactions;
    };

    const handleSort = (column: string) => {
        if (column === sortBy) {
            setSortOrder(sortOrder === 'asc' ? 'desc' : 'asc');
        } else {
            setSortBy(column);
            setSortOrder('asc');
        }
    };

    const sortedTransactions = [...transactions].sort((a, b) => {
        if (sortBy === 'amount') {
            return sortOrder === 'asc'
                ? a.transactionAmount.amount - b.transactionAmount.amount
                : b.transactionAmount.amount - a.transactionAmount.amount;
        } else if (sortBy === 'date') {
            return sortOrder === 'asc'
                ? new Date(a.transactionDateUtc).getTime() -
                new Date(b.transactionDateUtc).getTime()
                : new Date(b.transactionDateUtc).getTime() -
                new Date(a.transactionDateUtc).getTime();
        }
        return 0;
    });

    const groupedTransactions = groupBy(sortedTransactions, groupCriterion);

    return (
        <Table>
            <TableHead>
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
                {Object.keys(groupedTransactions).map((groupKey) => (
                    <Fragment key={groupKey}>
                        <TableRow sx={{ backgroundColor: theme.palette.background.default }}>
                            <TableCell colSpan={5} align={'center'} sx={{ borderBottom: 'none' }}>
                                <strong>{groupKey}</strong>
                            </TableCell>
                        </TableRow>
                        {groupedTransactions[groupKey].map((transaction) => (
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
                    </Fragment>
                ))}
            </TableBody>
        </Table>
    );
}