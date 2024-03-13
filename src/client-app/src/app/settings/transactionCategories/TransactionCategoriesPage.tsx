import AppOverlay from "../../../components/appOverlay/AppOverlay";
import {Grid, Paper, Typography, useMediaQuery} from "@mui/material";
import theme from "../../theme";
import {useStore} from "../../../stores/store";
import {useEffect, useState} from "react";
import useCategories from "../../../utils/hooks/useCategories";
import {observer} from "mobx-react-lite";
import TransactionCategoriesView from "./components/TransactionCategoriesView";
import {LoadingTile} from "./components/LoadingTile";
import {TransactionSubcategoriesView} from "./components/TransactionSubcategoriesView";

export default observer(function TransactionCategoriesPage() {
    const {categoryStore} = useStore()
    const transactionCategories = useCategories()
    const isMobile = useMediaQuery(theme.breakpoints.down('md'))
    const [initialLoading, setInitialLoading] = useState(true)

    useEffect(() => {
        !categoryStore.loading && setInitialLoading(false)
    }, [categoryStore.loading])

    const getIncomeCategories = () => transactionCategories.filter(tc => tc.type.toLowerCase() === 'income')
    const getOutcomeCategories = () => transactionCategories.filter(tc => tc.type.toLowerCase() === 'outcome')

    return (
        <AppOverlay>
            <Grid container spacing={isMobile ? 0 : 3} sx={{
                height:'100%',
                padding: 2,
                paddingBottom: 5,
                margin: 0,
                backgroundColor: theme.palette.background.paper,
                borderRadius: '20px',
                overflow:'auto',
                userSelect: 'none',
                maxWidth: '1920px',
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
                        categoryStore.selectedCategory &&
                        getIncomeCategories().some(c => c.transactionCategoryId === categoryStore.selectedCategory?.transactionCategoryId) ?
                            <TransactionSubcategoriesView
                                transactionSubcategories={categoryStore.selectedCategory.subcategories}
                                type={'income'}
                            />
                            :
                            <TransactionCategoriesView
                                transactionCategories={getIncomeCategories()}
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
                        categoryStore.selectedCategory &&
                        getOutcomeCategories().some(c => c.transactionCategoryId === categoryStore.selectedCategory?.transactionCategoryId) ?
                            <TransactionSubcategoriesView
                                transactionSubcategories={categoryStore.selectedCategory.subcategories}
                                type={'outcome'}
                            />
                            :
                            <TransactionCategoriesView
                                transactionCategories={getOutcomeCategories()}
                                type={'outcome'}
                            />
                }
            </Grid>
        </AppOverlay>
    );
})