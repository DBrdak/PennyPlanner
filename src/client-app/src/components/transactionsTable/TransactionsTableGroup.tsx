import {
    Button, Checkbox, IconButton,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow, Typography,
} from "@mui/material";
import SortableTableCell from "./SortableTableCell";
import {useState} from "react";
import {Transaction} from "../../models/transactions/transaction";
import formatDate from "../../utils/formatters/dateFormatter";
import {useLocation, useNavigate} from "react-router-dom";
import {observer} from "mobx-react-lite";
import {useStore} from "../../stores/store";
import {TransactionEntity} from "../../models/transactionEntities/transactionEntity";
import {Account} from "../../models/accounts/account";
import theme from "../../app/theme";
import {DeleteTwoTone} from "@mui/icons-material";
import formatNumber from "../../utils/formatters/numberFormatter";

interface TransactionsTableGroupProps {
    groupedTransactions: Record<string, Transaction[]>
    groupCriterion: string
    groupKey: string
    editMode: boolean
}

export default observer(function TransactionsTableGroup({groupedTransactions, groupCriterion, groupKey, editMode}: TransactionsTableGroupProps) {
    const [sortBy, setSortBy] = useState<string | null>(null);
    const [sortOrder, setSortOrder] = useState<'asc' | 'desc'>('asc');
    const navigate = useNavigate()
    const location = useLocation()
    const {accountStore, transactionEntityStore, transactionStore} = useStore()

    const handleCheckboxClick = (transactionId: string) => {
        if(transactionStore.transactionsIdToRemove.some(id => id === transactionId)){
            transactionStore.removeTransactionIdToRemove(transactionId)
        } else {
            transactionStore.setTransactionIdToRemove(transactionId)
        }
    };

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

    const handleDelete = () => {
        transactionStore.removeTransactions()
    }

    return (
        <TableContainer sx={{overflow: 'hidden'}}>
            <Table>
                <TableHead sx={{backgroundColor: '#121212', position: 'sticky', top: 0, zIndex: 1}}>
                    <TableRow>
                        {
                            location.pathname.startsWith('/transactions') && groupCriterion !== 'account' ?
                                <>
                                    <TableCell align={'center'} padding={'none'}>
                                        <IconButton color={'error'} onClick={handleDelete} sx={{
                                            width: '100%',
                                            height: '100%',
                                            borderRadius: 0,
                                            flexDirection: 'column'
                                        }}>
                                            <DeleteTwoTone />
                                            <Typography variant={'caption'}>Execute Delete</Typography>
                                        </IconButton>
                                    </TableCell>
                                    <TableCell align={'center'}>
                                        <strong>Account</strong>
                                    </TableCell>
                                </>
                                :
                                <TableCell />
                        }
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
                        const baseAccount = accountStore.getAccount(transaction.accountId)

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
                                <TableCell align={'center'}>
                                    {
                                        editMode &&
                                        <Checkbox
                                            icon={<DeleteTwoTone />}
                                            checkedIcon={<DeleteTwoTone color={'error'} />}
                                            color={'error'}
                                            checked={transactionStore.transactionsIdToRemove.some(id => id === transaction.transactionId)}
                                            onChange={() => handleCheckboxClick(transaction.transactionId)}
                                        />
                                    }
                                </TableCell>
                                {
                                    location.pathname.startsWith('/transactions') && groupCriterion !== 'account' &&
                                        <TableCell align={'center'}>
                                            {baseAccount?.name || '-'}
                                        </TableCell>
                                }
                                <TableCell align={'center'} sx={{color: transaction.transactionAmount.amount >= 0 ?
                                        theme.palette.success.light : theme.palette.error.light}}>
                                    {formatNumber(transaction.transactionAmount.amount)} {transaction.transactionAmount.currency}
                                </TableCell>
                                {
                                    groupCriterion !== 'category' &&
                                        <TableCell align={'center'}>
                                            <Button disabled={!transaction.category?.transactionCategoryId} color={'inherit'} sx={{borderRadius: '4px'}}
                                                    onClick={() =>
                                                        (transaction.category?.transactionCategoryId &&
                                                            navigate(`/statistics/transaction-categories/${transaction.category.transactionCategoryId}`))}>
                                                {transaction.category ? transaction.category.value : '-'}
                                            </Button>
                                        </TableCell>
                                }
                                {
                                    groupCriterion !== 'entity' &&
                                        <TableCell align={'center'}>
                                            <Button disabled={!account?.accountId && !transactionEntity?.transactionEntityId} color={'inherit'} sx={{borderRadius: '4px'}}
                                                    onClick={() =>
                                                    (account?.accountId &&
                                                        navigate(`/accounts/${account.accountId}`)) ||
                                                    (transactionEntity?.transactionEntityId &&
                                                        navigate(`/statistics/transaction-entities/${transactionEntity.transactionEntityId}`))}>
                                                {transactionEntity?.name || account?.name || '-'}
                                            </Button>
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