import {TransactionCategory} from "../../../../models/transactionCategories/transactionCategory";
import {Grid, IconButton, Paper, Typography} from "@mui/material";
import theme from "../../../theme";
import {BudgetedCategoryCreateForm} from "./BudgetedCategoryCreateForm";
import {observer} from "mobx-react-lite";
import {Add} from "@mui/icons-material";

interface BudgetedCategoryCreateCardProps {
    category?: TransactionCategory
}

export default observer(function BudgetedCategoryCreateCard({category}: BudgetedCategoryCreateCardProps) {

    const calcColor = (type: string) => type.toLowerCase() === 'income' ? theme.palette.success.light : theme.palette.error.main

    return (
        <Grid item xs={12} md={4} lg={3} sx={{
            height: '30%',
            minHeight: '400px'
        }}>
            <Paper sx={{
                height: '100%',
                minHeight: '400px',
                borderRadius: '20px',
                padding: theme.spacing(3),
                position: 'relative'
            }}>
                {
                    category ?
                        <>
                            <Grid item xs={12} sx={{height: '10%'}}>
                                <Typography variant={'h4'} textAlign={'center'}>
                                    {category.value}
                                </Typography>
                                <Typography variant={'subtitle1'} textAlign={'center'} sx={{color: calcColor(category.type)}}>
                                    {category.type}
                                </Typography>
                            </Grid>
                            <BudgetedCategoryCreateForm
                                category={category}
                                onSubmit={(values) => console.log(values)}
                                onCancel={() => console.log(category.transactionCategoryId)}
                            />
                        </>
                        :
                        <IconButton centerRipple={false} sx={{
                            position: 'absolute',
                            top: 0, right: 0, left: 0, bottom: 0,
                            borderRadius: '20px'
                        }}>
                            <Add />
                        </IconButton>
                }
            </Paper>
        </Grid>
    );
})