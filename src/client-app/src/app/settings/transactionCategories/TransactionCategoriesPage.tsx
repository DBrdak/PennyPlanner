import AppOverlay from "../../../components/appOverlay/AppOverlay";
import {Grid, Paper, Typography} from "@mui/material";
import theme from "../../theme";
import {useStore} from "../../../stores/store";
import {useEffect, useState} from "react";
import ConfirmModal from "../../../components/ConfirmModal";
import TransactionCategoryTile from "./components/TransactionCategoryTile";
import useCategories from "../../../utils/hooks/useCategories";
import {AddTransactionCategoryCommand} from "../../../models/requests/categories/addTransactionCategoryCommand";
import {observer} from "mobx-react-lite";
import TransactionCategoriesView from "./components/TransactionCategoriesView";
import {LoadingTile} from "./components/LoadingTile";
import {TransactionSubcategoriesView} from "./components/TransactionSubcategoriesView";
import {
    AddTransactionSubcategoryCommand
} from "../../../models/requests/subcategories/addTransactionSubcategoryCommand";

export default observer(function TransactionCategoriesPage() {
    const {categoryStore, subcategoryStore, modalStore} = useStore()
    const [loadingCategoriesId, setLoadingCategoriesId] = useState<string[]>([])
    const transactionCategories = useCategories()
    const [initialLoading, setInitialLoading] = useState(true)

    useEffect(() => {
        !categoryStore.loading && setInitialLoading(false)
    }, [categoryStore.loading])

    const getIncomeCategories = () => transactionCategories.filter(tc => tc.type.toLowerCase() === 'income')
    const getOutcomeCategories = () => transactionCategories.filter(tc => tc.type.toLowerCase() === 'outcome')

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

    function handleCreate(command: AddTransactionCategoryCommand | AddTransactionSubcategoryCommand) {

        if((command as AddTransactionCategoryCommand).type){
            setLoadingCategoriesId(prev => [...prev, (command as AddTransactionCategoryCommand).type])
            categoryStore.addTransactionCategory(command as AddTransactionCategoryCommand).then(() => {
                setLoadingCategoriesId(prev => prev.filter(id => id !== (command as AddTransactionCategoryCommand).type))
            })
        } else {
            setLoadingCategoriesId(prev => [...prev, command.value]) // TODO Fix loading
            subcategoryStore.addTransactionSubcategory(command as AddTransactionSubcategoryCommand).then(() => {
                setLoadingCategoriesId(prev => prev.filter(id => id !== command.value))
            })
        }

    }

    return (
        <AppOverlay>
            <Grid container spacing={3} sx={{
                height:'100%',
                padding: 2,
                paddingBottom: 5,
                margin: 0,
                backgroundColor: theme.palette.background.paper,
                borderRadius: '20px',
                overflow:'auto',
                userSelect: 'none',
            }}>
                <Paper sx={{
                    width: '100%',
                    height: theme.spacing(12),
                    display: 'flex',
                    justifyContent: 'center',
                    alignItems: 'center',
                    marginBottom: 3
                }}>
                    <Typography variant={'h3'}>
                        Income
                    </Typography>
                </Paper>
                {
                    initialLoading ?
                        <LoadingTile />
                        :
                        categoryStore.selectedCategory ?
                            <TransactionSubcategoriesView
                                handleDelete={handleDelete}
                                handleEdit={handleEdit}
                                handleCreate={handleCreate}
                                transactionSubcategories={categoryStore.selectedCategory.subcategories}
                                loadingSubcategoriesId={loadingCategoriesId}
                            />
                            :
                            <TransactionCategoriesView
                                handleDelete={handleDelete}
                                handleEdit={handleEdit}
                                handleCreate={handleCreate}
                                transactionCategories={getIncomeCategories()}
                                loadingCategoriesId={loadingCategoriesId}
                                type={'income'}
                            />
                }
                <Paper sx={{
                    width: '100%',
                    height: theme.spacing(12),
                    display: 'flex',
                    justifyContent: 'center',
                    alignItems: 'center',
                    marginBottom: 3
                }}>
                    <Typography variant={'h3'}>
                        Outcome
                    </Typography>
                </Paper>
                {
                    initialLoading ?
                        <LoadingTile />
                        :
                        categoryStore.selectedCategory ?
                            <TransactionSubcategoriesView
                                handleDelete={handleDelete}
                                handleEdit={handleEdit}
                                handleCreate={handleCreate}
                                transactionSubcategories={categoryStore.selectedCategory.subcategories}
                                loadingSubcategoriesId={loadingCategoriesId}
                            />
                            :
                            <TransactionCategoriesView
                                handleDelete={handleDelete}
                                handleEdit={handleEdit}
                                handleCreate={handleCreate}
                                transactionCategories={getOutcomeCategories()}
                                loadingCategoriesId={loadingCategoriesId}
                                type={'outcome'}
                            />
                }
            </Grid>
        </AppOverlay>
    );
})