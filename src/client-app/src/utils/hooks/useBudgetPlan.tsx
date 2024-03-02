import {useStore} from "../../stores/store";
import {useEffect, useState} from "react";
import {BudgetPlan} from "../../models/budgetPlans/budgetPlan";

const useBudgetPlan = (date: Date) => {
    const { budgetPlanStore } = useStore();
    const [budgetPlan, setBudgetPlan] = useState<BudgetPlan>()

    useEffect(() => {
        const loadBudgetPlan = async () => {
            await budgetPlanStore.loadBudgetPlan(date);
        };

        loadBudgetPlan().then(() => {
            setBudgetPlan(budgetPlanStore.budgetPlan)
        })

    }, [budgetPlanStore, date]);

    return budgetPlanStore.budgetPlan;
};

export default useBudgetPlan