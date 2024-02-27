import {CircularProgress, Grid} from "@mui/material";
import theme from "../../theme";
import {useState} from "react";
import {BudgetPlanDateChange} from "./dateChange/BudgetPlanDateChange";
import {BudgetedCategoriesContainer} from "./view/BudgetedCategoriesContainer";
import {useStore} from "../../../stores/store";
import {observer} from "mobx-react-lite";
import useBudgetPlan from "../../../utils/hooks/useBudgetPlan";
import BudgetPlanCreateContainer from "./create/BudgetPlanCreateContainer";

export default observer(function BudgetPlanContainer() {
    const [date, setDate] = useState(new Date())
    const {budgetPlanStore} = useStore()
    const budgetPlan = useBudgetPlan(date)

    return (
        <Grid container sx={{
            height:'100%',
            padding: theme.spacing(3),
            margin: 0,
            overflow: 'hidden',
            backgroundColor: theme.palette.background.paper,
            borderRadius: '20px',
            userSelect: 'none'
        }}>
            <Grid item xs={12} sx={{
                height: '10%',
                minHeight: '75px'
            }}>
                <BudgetPlanDateChange
                    date={date}
                    setDate={setDate}
                />
            </Grid>
            <Grid item xs={12} sx={{
                overflow: 'auto',
                maxHeight: '80%',
                textAlign: 'center',
                marginTop: `${theme.spacing(5)}`
            }}>
                {
                    budgetPlan ?
                        budgetPlan?.budgetedTransactionCategories.length > 0 ?
                            <BudgetedCategoriesContainer budgetedCategories={budgetPlan?.budgetedTransactionCategories} />
                            :
                            <BudgetPlanCreateContainer />
                        :
                        <CircularProgress />
                }
            </Grid>
        </Grid>
    );
})