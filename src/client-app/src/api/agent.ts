import axios, { AxiosResponse } from "axios"
import { toast } from "react-toastify"
import { router } from "../router/Routes"
import {NewAccountData} from "../models/requests/newAccountData";
import {AddTransactionEntityCommand} from "../models/requests/addTransactionEntityCommand";
import {AccountUpdateData} from "../models/requests/accountUpdateData";
import {BudgetedTransactionCategoryValues} from "../models/requests/budgetedTransactionCategoryValues";
import {UpdateBudgetPlanCategoryValues} from "../models/requests/updateBudgetPlanCategoryValues";
import {AddInternalTransactionCommand} from "../models/requests/addInternalTransactionCommand";
import {AddIncomeTransactionCommand} from "../models/requests/addIncomeTransactionCommand";
import {AddOutcomeTransactionCommand} from "../models/requests/addOutcomeTransactionCommand";

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)
    })
}

axios.defaults.baseURL = process.env.REACT_APP_API_URL;

const responseBody = <T> (response: AxiosResponse<T>) => response.data;

axios.interceptors.response.use(async(response) => {
        if(process.env.NODE_ENV === "development") {
            await sleep(1000)
        }
        return response
    }, (error) => {
        console.log(error.data)
        if (error.response) {
            if (error.response.data && error.response.data.name) {
                const errorMessage = error.response.data.name;
                toast.error(errorMessage);
                return Promise.reject();
            } else {
                switch(error.response.status) {
                    case 400:
                        if(error.response.config.method === 'get' && error.response.data.errors.hasOwnProperty('id')){
                            router.navigate('/not-found');
                        }
                        break;
                    case 401:
                        if( error.response.headers['www-authenticate']?.startsWith('Bearer error="invalid_token"')){
                            toast.error("Session expired - please login again")
                        }
                        else toast.error('unauthorized')
                        break;
                    case 403:
                        toast.error('forbidden')
                        break;
                    case 404:
                        router.navigate('/not-found');
                        break;
                    case 500:
                        router.navigate('/server-error');
                        break;
                }
            }
        }

        // Pass the error to the next handler
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
    getAccounts: () => axios.get('/accounts').then(responseBody),
    createAccount: (data: NewAccountData) => axios.post('/accounts', data).then(responseBody),
    updateAccount: (data: AccountUpdateData) => axios.put('/accounts', data).then(responseBody),
    deleteAccount: (accountId: string) => axios.delete(`/accounts/${accountId}`),
}

const budgetPlans = {
    getBudgetPlans: () => axios.get('/budget-plans').then(responseBody),
    createBudgetPlan: (period: DateTimeRange) => axios.post('/budget-plans', period).then(responseBody),
    updateBudgetPlan: (budgetPlanId: string, categories: BudgetedTransactionCategoryValues[]) =>
        axios.put(`/budget-plans/${budgetPlanId}`, categories).then(responseBody),
    updateBudgetPlanCategory: (budgetPlanId: string, budgetPlanCategory: string, values: UpdateBudgetPlanCategoryValues) =>
        axios.put(`/budget-plans/${budgetPlanId}/${budgetPlanCategory}`, values).then(responseBody),
}

const transactionEntities = {
    getTransactionEntities: () => axios.get('/transaction-entities').then(responseBody),
    createTransactionEntity: (command: AddTransactionEntityCommand) =>
        axios.post('/transaction-entities', command).then(responseBody),
    updateTransactionEntity: (transactionEntityId: string, data: string) =>
        axios.put(`/transaction-entities/${transactionEntityId}`, data).then(responseBody),
    deleteTransactionEntity: (transactionEntityId: string) =>
        axios.delete(`/transaction-entities/${transactionEntityId}`).then(responseBody),
}

const transactions = {
    createInternalTransaction: (command: AddInternalTransactionCommand) =>
        axios.post('/transactions/internal', command).then(responseBody),
    createIncomeTransaction: (command: AddIncomeTransactionCommand) =>
        axios.post('/transactions/income', command).then(responseBody),
    createOutcomeTransaction: (command: AddOutcomeTransactionCommand) =>
        axios.post('/transactions/outcome', command).then(responseBody),
    deleteTransaction: (transactionId: string) => axios.delete(`/transactions/${transactionId}`).then(responseBody),
}

const agent = {

}

export default agent;