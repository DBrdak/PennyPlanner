import AppOverlay from "../../../components/appOverlay/AppOverlay";
import {CircularProgress, Grid, Paper, Typography} from "@mui/material";
import theme from "../../theme";
import {useStore} from "../../../stores/store";
import {useEffect, useState} from "react";
import ConfirmModal from "../../../components/ConfirmModal";
import {TransactionCategoryTile} from "./components/TransactionCategoryTile";
import {AddTransactionCategoryTile} from "./components/AddTransactionCategoryTile";
import useCategories from "../../../utils/hooks/useCategories";
import {AddTransactionCategoryCommand} from "../../../models/requests/categories/addTransactionCategoryCommand";
import {observer} from "mobx-react-lite";

export default observer(function TransactionCategoriesPage() {
    const {categoryStore, modalStore} = useStore()
    const [tileLoadingIds, setTileLoadingIds] = useState<string[]>([])
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
                setTileLoadingIds(prev => [...prev, transactionCategoryId])
                categoryStore.deleteTransactionCategory(transactionCategoryId).then(() => {
                    setTileLoadingIds(prev => prev.filter(id => id !== transactionCategoryId))
                })
            }}
        />)
    }

    const handleEdit = (transactionCategoryId: string, newValue: string) => {
        setTileLoadingIds(prev => [...prev, transactionCategoryId])
        categoryStore.updateTransactionCategory(transactionCategoryId, newValue).then(() => {
            setTileLoadingIds(prev => prev.filter(id => id !== transactionCategoryId))
        })
    }

    function handleCreate(command: AddTransactionCategoryCommand) {
        setTileLoadingIds(prev => [...prev, command.type])
        categoryStore.addTransactionCategory(command).then(() => {
            setTileLoadingIds(prev => prev.filter(id => id !== command.type))
        })
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
                        <Grid item xs={12} sx={{
                            minHeight: '200px',
                            height: '33%',
                            marginBottom: 3,
                            display: 'flex',
                            justifyContent: 'center',
                            alignItems: 'center'
                        }}>
                            <CircularProgress />
                        </Grid>
                        :
                        <>
                            {
                                getIncomeCategories().map(transactionCategory => (
                                    <TransactionCategoryTile
                                        key={transactionCategory.transactionCategoryId}
                                        transactionCategory={transactionCategory}
                                        onDelete={handleDelete}
                                        onEdit={handleEdit}
                                        loading={tileLoadingIds.some(id => id === transactionCategory.transactionCategoryId)}
                                    />
                                ))
                            }
                            <AddTransactionCategoryTile
                                onCreate={handleCreate}
                                type={'income'}
                                loading={tileLoadingIds.some(id => id === 'income')}
                            />
                        </>
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
                        <Grid item xs={12} sx={{
                            minHeight: '200px',
                            height: '33%',
                            marginBottom: 3,
                            display: 'flex',
                            justifyContent: 'center',
                            alignItems: 'center'
                        }}>
                            <CircularProgress />
                        </Grid>
                        :
                        <>
                            {
                                getOutcomeCategories().map(transactionCategory => (
                                    <TransactionCategoryTile
                                        key={transactionCategory.transactionCategoryId}
                                        transactionCategory={transactionCategory}
                                        onDelete={handleDelete}
                                        onEdit={handleEdit}
                                        loading={tileLoadingIds.some(id => id === transactionCategory.transactionCategoryId)}
                                    />
                                ))
                            }
                            <AddTransactionCategoryTile
                                onCreate={handleCreate}
                                type={'outcome'}
                                loading={tileLoadingIds.some(id => id === 'outcome')}
                            />
                        </>
                }
            </Grid>
        </AppOverlay>
    );
})