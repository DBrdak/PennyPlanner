import {useStore} from "../../stores/store";
import {useNavigate, useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {Account} from "../../models/accounts/account";
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

    return budgetPlan;
};

export default useBudgetPlan