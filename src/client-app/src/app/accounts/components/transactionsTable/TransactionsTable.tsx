import {Box, Collapse, Paper, Stack, TableCell, TableRow, Typography} from "@mui/material";
import {Transaction} from "../../../../models/transactions/transaction";
import {Fragment, useState} from "react";
import theme from "../../../theme";
import TransactionsTableGroup from "./TransactionsTableGroup";
import formatDate from "../../../../utils/dateFormatter";
import {ExpandLess, ExpandLessTwoTone, ExpandMoreTwoTone} from "@mui/icons-material";
import TransactionsTableGroupHeader from "./TransactionsTableGroupHeader";
import {useNavigate} from "react-router-dom";

interface TransactionsTableProps {
    transactions: Transaction[]
    groupCriterion: string
    collapsedGroups: string[]
    setCollapsedGroups: (groupKeys: string[]) => void
}

export function TransactionsTable({ transactions, groupCriterion, collapsedGroups, setCollapsedGroups }: TransactionsTableProps) {
    const [hoveredGroup, setHoveredGroup] = useState<string | null>(null);
    const navigate = useNavigate()

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
                case 'entity':
                    criterion = 'recipientId' || 'senderId' || 'fromAccountId' || 'toAccountId'
                    key = transaction[criterion as keyof Transaction] as string || 'Private'
                    break
                case 'category':
                    key = transaction[criterion as keyof Transaction] as string || 'Unknown'
                    break
                default:
                    navigate('/not-found')
                    break
            }

            if (key && !groupedTransactions[key]) {
                groupedTransactions[key] = [];
            }

            key && groupedTransactions[key].push(transaction);
        });

        return groupedTransactions;
    };


    const groupedTransactions = groupBy(transactions, groupCriterion);

    const handleGroupCollapse = (groupKey: string) => {
        if(collapsedGroups.some(x => x === groupKey)) {
            const newCollapsedGroups = collapsedGroups.filter(x => x !== groupKey)
            setCollapsedGroups(newCollapsedGroups)
        } else {
            setCollapsedGroups([...collapsedGroups, groupKey])
        }
    }

    const handleGroupHover = (groupKey: string) => {
        setHoveredGroup(groupKey);
    };

    const isGroupHovered = (groupKey: string) => hoveredGroup === groupKey;
    const isGroupCollapsed = (groupKey: string) => collapsedGroups.some(x => x === groupKey)

    return (
        <Box sx={{width: '100%', height: '50%', overflow: 'auto'}}>
            {Object.keys(groupedTransactions).map((groupKey) => (
                <Box key={groupKey}>
                    <TransactionsTableGroupHeader
                        key={groupKey}
                        groupKey={groupKey}
                        isCollapsed={isGroupCollapsed(groupKey)}
                        isHovered={isGroupHovered(groupKey)}
                        onClick={() => handleGroupCollapse(groupKey)}
                        onMouseEnter={() => handleGroupHover(groupKey)}
                        onMouseLeave={() => handleGroupHover('')}
                    />
                    <Collapse in={!isGroupCollapsed(groupKey)}>
                        <TransactionsTableGroup groupedTransactions={groupedTransactions} groupCriterion={groupCriterion} groupKey={groupKey} />
                    </Collapse>
                </Box>
            ))}
        </Box>
    );
}