import { RouteObject } from "react-router"
import {createBrowserRouter} from 'react-router-dom'
import RedirectPage from "../components/RedirectPage";
import App from "../app/App";
import SignInPage from "../app/login/SignInPage";
import SignUpPage from "../app/register/SignUpPage";
import AccountsPage from "../app/accounts/AccountsPage";
import BudgetPlansPage from "../app/budget-plans/BudgetPlansPage";
import HomePage from "../app/home/HomePage";
import AddAccountPage from "../app/accounts/new/AddAccountPage";
import AccountDetailsPage from "../app/accounts/details/AccountDetailsPage";
import GoalsPage from "../app/goals/GoalsPage";
import StatisticsPage from "../app/statistics/StatisticsPage";
import SettingsPage from "../app/settings/SettingsPage";
import UserPage from "../app/user/UserPage";
import WelcomePage from "../app/welcome/WelcomePage";
import TransactionsPage from "../app/transactions/TransactionsPage";
import AddIncomePage from "../app/transactions/income/AddIncomePage";
import AddOutcomePage from "../app/transactions/outcome/AddOutcomePage";
import AddInternalTransactionPage from "../app/transactions/internal/AddInternalTransactionPage";
import CustomizePage from "../app/settings/customize/CustomizePage";
import RecurringTransactionsPage from "../app/settings/recurringTransactions/RecurringTransactionsPage";
import TransactionEntitiesPage from "../app/settings/transactionEntities/TransactionEntitiesPage";
import TransactionCategoriesPage from "../app/settings/transactionCategories/TransactionCategoriesPage";
import RequireAuth from "./requireAuth";

export const routes: RouteObject[] = [
  {
    path: '/',
    element: <App />,
    children: [
      {path: '', element: <WelcomePage />},
      {path: '/login', element: <SignInPage />},
      {path: '/register', element: <SignUpPage />},

      {element: <RequireAuth />, children: [
          {path: '/home', element: <HomePage />},

          {path: '/transactions', element: <TransactionsPage />},
          {path: '/transactions/income', element: <AddIncomePage />},
          {path: '/transactions/outcome', element: <AddOutcomePage />},
          {path: '/transactions/internal', element: <AddInternalTransactionPage />},

          {path: '/budget-plans', element: <BudgetPlansPage />},

          {path: '/accounts', element: <AccountsPage />},
          {path: '/accounts/new', element: <AddAccountPage />},
          {path: '/accounts/:accountId', element: <AccountDetailsPage />},
          {path: '/accounts/total', element: <AccountDetailsPage />},

          {path: '/goals', element: <GoalsPage />},

          {path: '/statistics', element: <StatisticsPage  />},

          {path: '/settings', element: <SettingsPage  />},
          {path: '/settings/transaction-categories', element: <TransactionCategoriesPage  />},
          {path: '/settings/transaction-entities', element: <TransactionEntitiesPage  />},
          {path: '/settings/recurring-transactions', element: <RecurringTransactionsPage  />},
          {path: '/settings/customize', element: <CustomizePage  />},

          {path: '/user', element: <UserPage />},
      ]},

      {path: '/logout', element: <RedirectPage text={'Logging out...'} />},
      {path: '*', element: <RedirectPage text={'We are sorry, the content you are looking for does not exist 😔'} />}
    ]
  }
]

export const router = createBrowserRouter(routes);