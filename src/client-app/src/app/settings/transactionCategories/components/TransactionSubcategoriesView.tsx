import {TransactionCategory} from "../../../../models/transactionCategories/transactionCategory";
import {AddTransactionCategoryCommand} from "../../../../models/requests/categories/addTransactionCategoryCommand";
import {
    AddTransactionSubcategoryCommand
} from "../../../../models/requests/subcategories/addTransactionSubcategoryCommand";
import {TransactionSubcategory} from "../../../../models/transactionSubcategories/transactionSubcategory";

interface TransactionSubcategoriesViewProps {
    handleDelete: (transactionSubcategoryId: string) => void,
    handleEdit: (transactionSubcategoryId: string, newValue: string) => void,
    handleCreate: (command: AddTransactionSubcategoryCommand) => void,
    transactionSubcategories: TransactionSubcategory[],
    loadingSubcategoriesId: string[]
}

export function TransactionSubcategoriesView({
                                                 handleDelete,
                                                 handleEdit,
                                                 handleCreate,
                                                 transactionSubcategories,
                                                 loadingSubcategoriesId
                                             }: TransactionSubcategoriesViewProps) {
    return (
        <></>
    );
}