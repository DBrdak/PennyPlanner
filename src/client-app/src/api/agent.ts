import axios, {AxiosResponse} from "axios"
import {toast} from "react-toastify"
import { router } from "../router/Routes"
import {NewAccountData} from "../models/requests/accounts/newAccountData";
import {AddTransactionEntityCommand} from "../models/requests/transactionEntities/addTransactionEntityCommand";
import {AccountUpdateData} from "../models/requests/accounts/accountUpdateData";
import {BudgetedTransactionCategoryValues} from "../models/requests/budgetPlans/budgetedTransactionCategoryValues";
import {UpdateBudgetPlanCategoryValues} from "../models/requests/budgetPlans/updateBudgetPlanCategoryValues";
import {AddInternalTransactionCommand} from "../models/requests/transactions/addInternalTransactionCommand";
import {AddIncomeTransactionCommand} from "../models/requests/transactions/addIncomeTransactionCommand";
import {AddOutcomeTransactionCommand} from "../models/requests/transactions/addOutcomeTransactionCommand";
import {Account} from "../models/accounts/account";
import {TransactionEntity} from "../models/transactionEntities/transactionEntity";
import {Transaction} from "../models/transactions/transaction";
import {TransactionCategory} from "../models/transactionCategories/transactionCategory";
import {AddTransactionCategoryCommand} from "../models/requests/categories/addTransactionCategoryCommand";
import {AddTransactionSubcategoryCommand} from "../models/requests/subcategories/addTransactionSubcategoryCommand";

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)
    })
}

axios.defaults.baseURL = process.env.REACT_APP_API_URL

const responseBody = <T> (response: AxiosResponse<T>) => response.data;

axios.interceptors.response.use(async(response) => {

        if(process.env.NODE_ENV === "development") {
            await sleep(1000)
        }
        return response
    }, (error) => {
        if (error) {
            const errorMessage = error.response.data.error.name || error.response.data.name
            const errorMessages = errorMessage.split('\n')
            console.log(error)
            switch(error.response.status) {
                case 400:
                    errorMessages.forEach(toast.error)
                    return Promise.reject();
                case 401:
                    if( error.response.headers['www-authenticate']?.startsWith('Bearer error="invalid_token"')){
                        toast.error("Session expired - please login again")
                    }
                    else toast.error('Unauthorized')
                    break;
                case 403:
                    errorMessages.forEach(toast.error)
                    return Promise.reject();
                case 404:
                    if(error.response.config.method === 'get' && error.response.data.errors.hasOwnProperty('id')){
                        router.navigate('/not-found');
                    }
                    errorMessages.forEach(toast.error)
                    return Promise.reject();
                case 500:
                    router.navigate('/server-error');
                    break;
            }
        }

        return Promise.reject(error);
    }
);

const requests = {
    get: <T> (url: string) => axios.get<T>(url).then(responseBody),
    post: <T> (url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T> (url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    delete: <T> (url: string) => axios.delete<T>(url).then(responseBody)
}

const accounts = {
    getAccounts: () => axios.get<Account[]>('/accounts').then(responseBody),
    createAccount: (data: NewAccountData) => axios.post('/accounts', data),
    updateAccount: (data: AccountUpdateData) => axios.put(`/accounts/${data.accountId}`, data),
    deleteAccount: (accountId: string) => axios.delete(`/accounts/${accountId}`),
}

const budgetPlans = {
    getBudgetPlans: () => axios.get('/budget-plans').then(responseBody),
    createBudgetPlan: (period: DateTimeRange) => axios.post('/budget-plans', period),
    updateBudgetPlan: (budgetPlanId: string, categories: BudgetedTransactionCategoryValues[]) =>
        axios.put(`/budget-plans/${budgetPlanId}`, categories),
    updateBudgetPlanCategory: (budgetPlanId: string, budgetPlanCategory: string, values: UpdateBudgetPlanCategoryValues) =>
        axios.put(`/budget-plans/${budgetPlanId}/${budgetPlanCategory}`, values),
}

const transactionEntities = {
    getTransactionEntities: () => axios.get<TransactionEntity[]>('/transaction-entities').then(responseBody),
    createTransactionEntity: (command: AddTransactionEntityCommand) =>
        axios.post('/transaction-entities', command),
    updateTransactionEntity: (transactionEntityId: string, newName: string) =>
        axios.put(`/transaction-entities/${transactionEntityId}`, {id: transactionEntityId, newName: newName})
            ,
    deleteTransactionEntity: (transactionEntityId: string) =>
        axios.delete(`/transaction-entities/${transactionEntityId}`),
}

const transactionCategories = {
    getTransactionCategories: () =>
        axios.get<TransactionCategory[]>('/transaction-categories').then(responseBody),
    createTransactionCategory: (command: AddTransactionCategoryCommand) =>
        axios.post('/transaction-categories', command),
    updateTransactionCategory: (categoryId: string, newValue: string) =>
        axios.put(`/transaction-categories/${categoryId}`, {transactionCategoryId: categoryId, newValue: newValue}),
    deleteTransactionCategory: (transactionCategoryId: string) =>
        axios.delete(`/transaction-categories/${transactionCategoryId}`),
}

const transactionSubcategories = {
    addTransactionSubcategory: (command: AddTransactionSubcategoryCommand) =>
        axios.post('/transaction-subcategories', command),
    updateTransactionSubcategory: (transactionSubcategoryId: string, newValue: string) =>
        axios.put(`/transaction-subcategories/${transactionSubcategoryId}`, { transactionSubcategoryId: transactionSubcategoryId, newValue: newValue }),
    removeTransactionSubcategory: (transactionSubcategoryId: string) =>
        axios.delete(`/transaction-subcategories/${transactionSubcategoryId}`),
}

const transactions = {
    getTransactions: () => axios.get<Transaction[]>('/transactions').then(responseBody),
    createInternalTransaction: (command: AddInternalTransactionCommand) =>
        axios.post('/transactions/internal', command),
    createIncomeTransaction: (command: AddIncomeTransactionCommand) =>
        axios.post('/transactions/income', command),
    createOutcomeTransaction: (command: AddOutcomeTransactionCommand) =>
        axios.post('/transactions/outcome', command),
    deleteTransaction: (transactionId: string) => axios.delete(`/transactions/${transactionId}`),
}

const agent = {
    accounts,
    budgetPlans,
    transactionEntities,
    transactionCategories,
    transactionSubcategories,
    transactions,
}

export default agent;