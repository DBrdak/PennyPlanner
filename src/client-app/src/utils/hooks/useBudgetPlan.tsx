import {useStore} from "../../stores/store";
import {useEffect, useState} from "react";
import {BudgetPlan} from "../../models/budgetPlans/budgetPlan";

const useBudgetPlan = (date: Date) => {
    const { budgetPlanStore } = useStore();
    const [budgetPlan, setBudgetPlan] = useState<BudgetPlan>()

    useEffect(() => {
        budgetPlanStore.loadBudgetPlan(date).then(() =>{
            setBudgetPlan(budgetPlanStore.budgetPlan)
        })

    }, [budgetPlanStore, date]);

    return budgetPlanStore.budgetPlan;
};

export default useBudgetPlan