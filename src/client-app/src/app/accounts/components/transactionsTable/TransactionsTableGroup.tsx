import {
    Button,
    Collapse,
    Paper,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Typography
} from "@mui/material";
import SortableTableCell from "./SortableTableCell";
import {useState} from "react";
import {Transaction} from "../../../../models/transactions/transaction";
import formatDate from "../../../../utils/dateFormatter";
import {useNavigate} from "react-router-dom";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../../stores/store";
import {TransactionEntity} from "../../../../models/transactionEntities/transactionEntity";
import {Account} from "../../../../models/accounts/account";
import theme from "../../../theme";

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
        <TableContainer sx={{overflow: 'hidden'}}>
            <Table>
                <TableHead sx={{backgroundColor: '#121212', position: 'sticky', top: 0, zIndex: 1}}>
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
                            groupCriterion !== 'category' &&
                            <TableCell align={'center'}>
                                <strong>Category</strong>
                            </TableCell>
                        }
                        {
                            groupCriterion !== 'entity' &&
                            <TableCell align={'center'}>
                                <strong>Entity</strong>
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
                        const entityQueryParams =
                            (transaction.fromAccountId && {variant: 1, id: transaction.fromAccountId}) ||
                            (transaction.toAccountId && {variant: 1, id: transaction.toAccountId}) ||
                            (transaction.senderId && {variant: 2, id: transaction.senderId}) ||
                            (transaction.recipientId && {variant: 2, id: transaction.recipientId})

                        let account: Account | undefined
                        let transactionEntity: TransactionEntity | undefined

                        if(entityQueryParams) {
                            switch (entityQueryParams?.variant) {
                                case 1:
                                    account = accountStore.getAccount(entityQueryParams.id!)
                                    break
                                case 2:
                                    transactionEntity = transactionEntityStore.getTransactionEntity(entityQueryParams.id!)
                                    break
                                default:
                                    break
                            }
                        }

                        return (
                            <TableRow key={transaction.transactionId}>
                                <TableCell width={'5%'} align={'center'} />
                                <TableCell width={'20%'} align={'center'} sx={{color: transaction.transactionAmount.amount >= 0 ?
                                        theme.palette.success.light : theme.palette.error.light}}>
                                    {transaction.transactionAmount.amount.toFixed(2)} {transaction.transactionAmount.currency}
                                </TableCell>
                                {groupCriterion !== 'category' && <TableCell width={'20%'} align={'center'}>{transaction.category}</TableCell>}
                                {
                                    groupCriterion !== 'entity' &&
                                    <TableCell width={'20%'} align={'center'}>
                                        <Button disabled={!account?.accountId && !transactionEntity?.transactionEntityId} color={'inherit'} sx={{borderRadius: '4px'}}
                                                onClick={() =>
                                                (account?.accountId &&
                                                    navigate(`/accounts/${account.accountId}`)) ||
                                                (transactionEntity?.transactionEntityId &&
                                                    navigate(`/settings/transaction-entities/${transactionEntity.transactionEntityId}`))}>
                                            {transactionEntity?.name || account?.name || '-'}
                                        </Button>
                                    </TableCell>
                                }
                                <TableCell width={'35%'} align={'center'}>{formatDate(transaction.transactionDateUtc)}</TableCell>
                            </TableRow>
                        )})}
                </TableBody>
            </Table>
        </TableContainer>
    );
})