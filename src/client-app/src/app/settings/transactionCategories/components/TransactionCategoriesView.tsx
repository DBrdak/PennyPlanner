import TransactionCategoryTile from "./TransactionCategoryTile";
import {AddTransactionCategoryTile} from "./AddTransactionCategoryTile";
import {AddTransactionCategoryCommand} from "../../../../models/requests/categories/addTransactionCategoryCommand";
import {TransactionCategory} from "../../../../models/transactionCategories/transactionCategory";

interface TransactionCategoriesViewProps {
    handleDelete: (transactionCategoryId: string) => void
    handleEdit: (transactionCategoryId: string, newValue: string) => void
    handleCreate: (command: AddTransactionCategoryCommand) => void
    transactionCategories: TransactionCategory[]
    loadingCategoriesId: string[]
    type: 'income' | 'outcome'
}

export default function TransactionCategoriesView({
        transactionCategories,
        loadingCategoriesId,
        handleEdit,
        handleDelete,
        handleCreate,
        type
    }: TransactionCategoriesViewProps) {
    return (
        <>
            {
                transactionCategories.map(transactionCategory => (
                    <TransactionCategoryTile
                        key={transactionCategory.transactionCategoryId}
                        transactionCategory={transactionCategory}
                        onDelete={handleDelete}
                        onEdit={handleEdit}
                        loading={loadingCategoriesId.some(id => id === transactionCategory.transactionCategoryId)}
                    />
                ))
            }
            <AddTransactionCategoryTile
                onCreate={handleCreate}
                type={type}
                loading={loadingCategoriesId.some(id => id === type)}
            />
        </>
    );
}