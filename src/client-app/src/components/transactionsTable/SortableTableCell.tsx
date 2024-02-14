import {TableCell, TableSortLabel} from "@mui/material";

interface SortableTableCellProps {
    label: string;
    column: string;
    sortOrder: 'asc' | 'desc';
    sortBy: string | null;
    onSort: (column: string) => void;
}

const SortableTableCell: React.FC<SortableTableCellProps> = ({
                                                                 label,
                                                                 column,
                                                                 sortOrder,
                                                                 sortBy,
                                                                 onSort,
                                                             }) => {
    const isSorted = sortBy === column;
    const isAsc = sortOrder === 'asc';

    return (
        <TableCell align={'center'} sortDirection={isSorted ? sortOrder : false}>
            <TableSortLabel active={isSorted} direction={isAsc ? 'asc' : 'desc'} onClick={() => onSort(column)}>
                {label}
            </TableSortLabel>
        </TableCell>
    );
};
export default SortableTableCell;