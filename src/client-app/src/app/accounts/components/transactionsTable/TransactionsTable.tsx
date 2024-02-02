import {Box, Collapse} from "@mui/material";
import {Transaction} from "../../../../models/transactions/transaction";
import {useState} from "react";
import TransactionsTableGroup from "./TransactionsTableGroup";
import TransactionsTableGroupHeader from "./TransactionsTableGroupHeader";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../../stores/store";

interface TransactionsTableProps {
    groupedTransactions: Record<string, Transaction[]>
    groupCriterion: string
    collapsedGroups: string[]
    setCollapsedGroups: (groupKeys: string[]) => void
    editMode: boolean
}

export default observer(function TransactionsTable({ groupedTransactions, groupCriterion, collapsedGroups, setCollapsedGroups, editMode }: TransactionsTableProps) {
    const [hoveredGroup, setHoveredGroup] = useState<string | null>(null);
    const {transactionEntityStore} = useStore()

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
        <Box sx={{width: '100%',  height: '100%', overflow: 'auto'}}>
            {Object.keys(groupedTransactions).map((groupKey) => {
                let groupKeyName

                if(groupCriterion === 'entity') {
                    groupKeyName = transactionEntityStore.getTransactionEntity(groupKey)?.name || groupKey
                } else {
                    groupKeyName = groupKey
                }

                return (
                    <Box key={groupKey}>
                        <TransactionsTableGroupHeader
                            groupKey={groupKeyName}
                            isCollapsed={isGroupCollapsed(groupKey)}
                            isHovered={isGroupHovered(groupKey)}
                            onClick={() => handleGroupCollapse(groupKey)}
                            onMouseEnter={() => handleGroupHover(groupKey)}
                            onMouseLeave={() => handleGroupHover('')}
                        />
                        <Collapse in={!isGroupCollapsed(groupKey)}>
                            <TransactionsTableGroup
                                groupedTransactions={groupedTransactions}
                                groupCriterion={groupCriterion} groupKey={groupKey}
                                editMode={editMode}
                            />
                        </Collapse>
                    </Box>
                    )
            })}
        </Box>
    );
})