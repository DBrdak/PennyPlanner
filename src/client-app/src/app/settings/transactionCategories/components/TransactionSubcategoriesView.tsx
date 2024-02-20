import {TransactionCategory} from "../../../../models/transactionCategories/transactionCategory";
import {AddTransactionCategoryCommand} from "../../../../models/requests/categories/addTransactionCategoryCommand";
import {
    AddTransactionSubcategoryCommand
} from "../../../../models/requests/subcategories/addTransactionSubcategoryCommand";
import {TransactionSubcategory} from "../../../../models/transactionSubcategories/transactionSubcategory";
import {useStore} from "../../../../stores/store";
import {useState} from "react";
import ConfirmModal from "../../../../components/ConfirmModal";
import TransactionCategoryTile from "./TransactionCategoryTile";
import {AddTransactionCategoryTile} from "./AddTransactionCategoryTile";
import {Grid} from "@mui/material";

interface TransactionSubcategoriesViewProps {
    transactionSubcategories: TransactionSubcategory[]
    type: 'income' | 'outcome'
}

export function TransactionSubcategoriesView({transactionSubcategories, type}: TransactionSubcategoriesViewProps) {
    const {subcategoryStore, categoryStore, modalStore} = useStore()
    const [loadingSubcategoriesId, setLoadingSubcategoriesId] = useState<string[]>([])

    const handleDelete = (transactionSubcategoryId: string) => {
        const transactionCategoryValue = transactionSubcategories
            .find(tc => tc.transactionSubcategoryId === transactionSubcategoryId)?.value

        modalStore.openModal(<ConfirmModal
            important
            text={`You are about to delete ${transactionCategoryValue}. All related transactions and budget plans will lose data about transaction subcategory. Are you sure you want to proceed?`}
            onConfirm={() => {
                setLoadingSubcategoriesId(prev => [...prev, transactionSubcategoryId])
                subcategoryStore.deleteTransactionSubcategory(transactionSubcategoryId).then(async () => {
                    await categoryStore.loadCategories()
                    setLoadingSubcategoriesId(prev => prev.filter(id => id !== transactionSubcategoryId))
                })
            }}
        />)
    }

    const handleEdit = (transactionSubcategoryId: string, newValue: string) => {
        setLoadingSubcategoriesId(prev => [...prev, transactionSubcategoryId])
        subcategoryStore.updateTransactionSubcategory(transactionSubcategoryId, newValue).then( async () => {
            await categoryStore.loadCategories()
            setLoadingSubcategoriesId(prev => prev.filter(id => id !== transactionSubcategoryId))
        })
    }

    function handleCreate(command: AddTransactionSubcategoryCommand) {

        setLoadingSubcategoriesId(prev => [...prev, type])
        subcategoryStore.addTransactionSubcategory(command).then(async () => {
            await categoryStore.loadCategories()
            setLoadingSubcategoriesId(prev => prev.filter(id => id !== type))
        })
    }

    return (
        <>
            <Grid item xs={12} sx={{
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center'
            }}>
                <TransactionCategoryTile
                    transactionCategory={categoryStore.selectedCategory!}
                    loading={false}
                />
            </Grid>
            {
                transactionSubcategories.map(subcategory => (
                    <TransactionCategoryTile
                        key={subcategory.transactionSubcategoryId}
                        transactionCategory={subcategory}
                        onDelete={handleDelete}
                        onEdit={handleEdit}
                        loading={loadingSubcategoriesId.some(id => id === subcategory.transactionSubcategoryId)}
                    />
                ))
            }
            <AddTransactionCategoryTile
                onCreate={(command) => handleCreate(command as TransactionSubcategory)}
                type={type}
                loading={loadingSubcategoriesId.some(id => id === type)}
                categoryId={categoryStore.selectedCategory?.transactionCategoryId}
            />
        </>
    );
}