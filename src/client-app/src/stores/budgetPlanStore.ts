import {makeAutoObservable} from "mobx";
import {BudgetPlan} from "../models/budgetPlans/budgetPlan";
import agent from "../api/agent";
import {isDateTimeRangeContainsDate} from "../utils/calculators/dateCalculator";
import {BudgetedTransactionCategoryValues} from "../models/requests/budgetPlans/budgetedTransactionCategoryValues";
import {UpdateBudgetPlanCategoryValues} from "../models/requests/budgetPlans/updateBudgetPlanCategoryValues";

export default class BudgetPlanStore {
    private budgetPlansRegistry: Map<string, BudgetPlan> = new Map<string, BudgetPlan>()
    newBudgetedCategoriesRegistry: Map<string, BudgetedTransactionCategoryValues> = new Map<string, BudgetedTransactionCategoryValues>()
    onDate: Date = new Date()
    loading: boolean = false
    editMode: boolean = false

    constructor() {
        makeAutoObservable(this);
    }

    get budgetPlans() {
        return Array.from(this.budgetPlansRegistry.values())
    }

    get budgetPlan() {
        return this.budgetPlans.find(bp => isDateTimeRangeContainsDate(bp.budgetPeriod, this.onDate))
    }

    get newBudgetedCategories() {
        return Array.from(this.newBudgetedCategoriesRegistry.values())
    }

    private setLoading(state: boolean) {
        this.loading = state
    }

    private setBudgetPlan(budgetPlan: BudgetPlan) {
        this.budgetPlansRegistry.delete(this.onDate.toISOString())
        this.budgetPlansRegistry.set(this.onDate.toISOString(), budgetPlan)
    }

    async loadBudgetPlan(onDate: Date) {
        this.setLoading(true)
        this.setOnDate(onDate)

        const params = new URLSearchParams()
        params.append('onDate', onDate.toISOString())

        try {
            const budgetPlan = await agent.budgetPlans.getBudgetPlan(params)

            this.setBudgetPlan(budgetPlan)
            this.newBudgetedCategoriesRegistry.clear()
        } catch (e) {

        } finally {
            this.setLoading(false)
        }
    }

    async setNewBudgetPlan() {

        if(!this.isNewBudgetPlanCreatePossible()) {
            return
        }

        this.setLoading(true)

        try {
            await agent.budgetPlans.setBudgetPlan({
                budgetPlanForDate: this.onDate,
                budgetedTransactionCategoryValues: this.newBudgetedCategories
            })
            await this.loadBudgetPlan(this.onDate)
        } catch (e) {

        } finally {
            this.setLoading(false)
        }
    }

    async updateBudgetPlan(categoryId: string, updateValues: UpdateBudgetPlanCategoryValues) {
        if(!this.budgetPlan) {
            return
        }

        this.setLoading(true)
        try{
            await agent.budgetPlans.updateBudgetPlanCategory(this.budgetPlan.budgetPlanId, categoryId, updateValues)
            await this.loadBudgetPlan(this.onDate)
        } catch (e) {

        } finally {
            this.setLoading(false)
        }
    }

    private setOnDate(date: Date) {
        this.onDate = date
    }

    setBudgetedCategory(budgetedCategory: BudgetedTransactionCategoryValues) {
        this.newBudgetedCategoriesRegistry.set(budgetedCategory.categoryValue, budgetedCategory)
    }

    removeBudgetedCategory(categoryValue: string) {
        this.newBudgetedCategoriesRegistry.delete(categoryValue)
    }

    isNewBudgetPlanCreatePossible() {
        return this.newBudgetedCategoriesRegistry.size > 0
    }

    setEditMode(state: boolean) {
        this.editMode = state
    }
}