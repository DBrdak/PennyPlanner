import TransactionCategoryTile from "./TransactionCategoryTile";
import {AddTransactionCategoryTile} from "./AddTransactionCategoryTile";
import {AddTransactionCategoryCommand} from "../../../../models/requests/categories/addTransactionCategoryCommand";
import {TransactionCategory} from "../../../../models/transactionCategories/transactionCategory";
import ConfirmModal from "../../../../components/ConfirmModal";
import {
    AddTransactionSubcategoryCommand
} from "../../../../models/requests/subcategories/addTransactionSubcategoryCommand";
import {useStore} from "../../../../stores/store";
import {useState} from "react";
import {observer} from "mobx-react-lite";

interface TransactionCategoriesViewProps {
    transactionCategories: TransactionCategory[]
    type: 'income' | 'outcome'
}

export default observer (function TransactionCategoriesView({
        transactionCategories,
        type
    }: TransactionCategoriesViewProps) {
    const {categoryStore, modalStore} = useStore()
    const [loadingCategoriesId, setLoadingCategoriesId] = useState<string[]>([])

    const handleDelete = (transactionCategoryId: string) => {
        const transactionCategoryValue = transactionCategories
            .find(tc => tc.transactionCategoryId === transactionCategoryId)?.value

        modalStore.openModal(<ConfirmModal
            important
            text={`You are about to delete ${transactionCategoryValue}. All related transactions and budget plans will lose data about transaction category. Are you sure you want to proceed?`}
            onConfirm={() => {
                setLoadingCategoriesId(prev => [...prev, transactionCategoryId])
                categoryStore.deleteTransactionCategory(transactionCategoryId).then(() => {
                    setLoadingCategoriesId(prev => prev.filter(id => id !== transactionCategoryId))
                })
            }}
        />)
    }

    const handleEdit = (transactionCategoryId: string, newValue: string) => {
        setLoadingCategoriesId(prev => [...prev, transactionCategoryId])
        categoryStore.updateTransactionCategory(transactionCategoryId, newValue).then(() => {
            setLoadingCategoriesId(prev => prev.filter(id => id !== transactionCategoryId))
        })
    }

    function handleCreate(command: AddTransactionCategoryCommand) {

        setLoadingCategoriesId(prev => [...prev, (command as AddTransactionCategoryCommand).type])
        categoryStore.addTransactionCategory(command as AddTransactionCategoryCommand).then(() => {
            setLoadingCategoriesId(prev => prev.filter(id => id !== (command as AddTransactionCategoryCommand).type))
        })
    }

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
                onCreate={(command) => handleCreate(command as TransactionCategory)}
                type={type}
                loading={loadingCategoriesId.some(id => id === type)}
            />
        </>
    );
})