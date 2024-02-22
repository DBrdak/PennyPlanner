import {Grid} from "@mui/material";
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
            padding: theme.spacing(2),
            margin: 0,
            backgroundColor: theme.palette.background.paper,
            borderRadius: '20px',
            overflow:'auto',
            userSelect: 'none'
        }}>
            <Grid item xs={12} sx={{
                height: '10%',
                minHeight: '100px'
            }}>
                <BudgetPlanDateChange
                    date={date}
                    setDate={setDate}
                />
            </Grid>
            <Grid item xs={12} sx={{
                height: '90%'
            }}>
                {
                    budgetPlan?.budgetedTransactionCategories ?
                        <BudgetedCategoriesContainer budgetedCategories={budgetPlan?.budgetedTransactionCategories} />
                        :
                        <BudgetPlanCreateContainer />
                }
            </Grid>
        </Grid>
    );
})