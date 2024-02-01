import {Collapse, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography} from "@mui/material";
import SortableTableCell from "./SortableTableCell";
import {useState} from "react";
import {Transaction} from "../../../../models/transactions/transaction";
import formatDate from "../../../../utils/dateFormatter";
import {useNavigate} from "react-router-dom";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../../stores/store";
import {TransactionEntity} from "../../../../models/transactionEntities/transactionEntity";
import {Account} from "../../../../models/accounts/account";

interface TransactionsTableGroupProps {
    groupedTransactions: Record<string, Transaction[]>
    groupCriterion: string
    groupKey: string
}

export default observer(function TransactionsTableGroup({groupedTransactions, groupCriterion, groupKey}: TransactionsTableGroupProps) {
    const [sortBy, setSortBy] = useState<string | null>(null);
    const [sortOrder, setSortOrder] = useState<'asc' | 'desc'>('asc');
    const navigate = useNavigate()
    const {accountStore, transactionEntityStore} = useStore()

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
                            groupCriterion === 'entity' ||
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
                    {sortTransactionGroup(groupedTransactions[groupKey]).map((transaction) => {
                        const entityQueryParams = {variant: 1, id: transaction.fromAccountId} ||
                            {variant: 1, id: transaction.toAccountId} ||
                            {variant: 2, id: transaction.senderId} ||
                            {variant: 2, id: transaction.recipientId}
                        let account: Account | undefined
                        let transactionEntity: TransactionEntity | undefined

                        switch(entityQueryParams.variant){
                            case 1:
                                account = accountStore.getAccount(entityQueryParams.id!)
                                break
                            case 2:
                                transactionEntity = transactionEntityStore.getTransactionEntity(entityQueryParams.id!)
                                break
                            default:
                                break
                        }

                        return (
                            <TableRow key={transaction.transactionId}>
                                <TableCell />
                                <TableCell align={'center'}>
                                    {transaction.transactionAmount.amount} {transaction.transactionAmount.currency}
                                </TableCell>
                                {groupCriterion === 'category' || <TableCell align={'center'}>{transaction.category}</TableCell>}
                                {
                                    groupCriterion === 'entity' ||
                                    <TableCell align={'center'}>
                                        <Typography onClick={() =>
                                            (account?.accountId && navigate(`/accounts/${account.accountId}`)) ||
                                            (transactionEntity?.transactionEntityId && navigate(`/settings/transaction-entities/${transactionEntity.transactionEntityId}`))}
                                        >
                                            {transactionEntity?.name || account?.name || '-'}
                                        </Typography>
                                    </TableCell>
                                }
                                <TableCell align={'center'}>{formatDate(transaction.transactionDateUtc)}</TableCell>
                            </TableRow>
                        )})}
                </TableBody>
            </Table>
        </TableContainer>
    );
})