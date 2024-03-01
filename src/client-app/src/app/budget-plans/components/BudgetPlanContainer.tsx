import {CircularProgress, Grid} from "@mui/material";
import theme from "../../theme";
import {useState} from "react";
import {BudgetPlanDateChange} from "./dateChange/BudgetPlanDateChange";
import BudgetedCategoriesContainer from "./view/BudgetedCategoriesContainer";
import {useStore} from "../../../stores/store";
import {observer} from "mobx-react-lite";
import useBudgetPlan from "../../../utils/hooks/useBudgetPlan";
import BudgetPlanCreateContainer from "./create/BudgetPlanCreateContainer";
import {BudgetPlanEditButton} from "./view/BudgetPlanEditButton";

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
            userSelect: 'none',
            maxWidth: '1920px'
        }}>
            <Grid item xs={12} sx={{
                height: '10%',
                minHeight: '75px'
            }}>
                <BudgetPlanDateChange
                    date={date}
                    setDate={setDate}
                    prevDateAccessible={false}
                />
            </Grid>
            <Grid item xs={12} sx={{
                overflow: 'auto',
                height: '80%',
                textAlign: 'center',
                position: 'relative',
                marginTop: `${theme.spacing(5)}`,
            }}>
                {
                    budgetPlan ?
                        budgetPlan?.budgetedTransactionCategories.length > 0 ?
                            <BudgetedCategoriesContainer
                                budgetedCategories={budgetPlan?.budgetedTransactionCategories}

                            />
                            :
                            <BudgetPlanCreateContainer />
                        :
                        <CircularProgress />
                }
            </Grid>
        </Grid>
    );
})