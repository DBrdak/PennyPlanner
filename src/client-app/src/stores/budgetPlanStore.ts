import {makeAutoObservable} from "mobx";
import {BudgetPlan} from "../models/budgetPlans/budgetPlan";
import agent from "../api/agent";
import {DateTimeRange} from "../models/shared/dateTimeRange";
import {isDateTimeRangeContainsDate} from "../utils/calculators/dateCalculator";

export default class BudgetPlanStore {
    private budgetPlansRegistry: Map<string, BudgetPlan> = new Map<string, BudgetPlan>()
    onDate: Date = new Date()
    loading: boolean = false

    constructor() {
        makeAutoObservable(this);
    }

    get budgetPlans() {
        return Array.from(this.budgetPlansRegistry.values())
    }

    get budgetPlan() {
        return this.budgetPlans.find(bp => isDateTimeRangeContainsDate(bp.budgetPeriod, this.onDate))
    }

    private setLoading(state: boolean) {
        this.loading = state
    }

    private setBudgetPlan(budgetPlan: BudgetPlan) {
        this.budgetPlansRegistry.set(budgetPlan.budgetPlanId, budgetPlan)
    }

    async loadBudgetPlan(onDate: Date) {
        this.setLoading(true)
        this.setOnDate(onDate)

        if(this.budgetPlan) {
            return
        }

        const params = new URLSearchParams()
        params.append('onDate', this.onDate.toISOString())

        try {
            const budgetPlan = await agent.budgetPlans.getBudgetPlan(params)
            budgetPlan && this.setBudgetPlan(budgetPlan)
        } catch (e) {
            console.log(e)
        } finally {
            this.setLoading(false)
        }
    }

    setOnDate(date: Date) {
        this.onDate = date
    }
}