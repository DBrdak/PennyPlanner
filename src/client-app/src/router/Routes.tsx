import { RouteObject } from "react-router"
import {createBrowserRouter} from 'react-router-dom'
import NotFoundPage from "../components/NotFoundPage";
import App from "../app/App";
import SignInPage from "../app/login/SignInPage";
import SignUpPage from "../app/register/SignUpPage";
import {HomePage} from "../app/home/HomePage";
import {BudgetPlansPage} from "../app/budget-plans/BudgetPlansPage";
import {AccountsPage} from "../app/accounts/AccountsPage";
import {GoalsPage} from "../app/goals/GoalsPage";
import {StatisticsPage} from "../app/statistics/StatisticsPage";
import {SettingsPage} from "../app/settings/SettingsPage";
import {UserPage} from "../app/user/UserPage";
import {AddAccount} from "../app/accounts/new/AddAccount";
import {BrowseAccounts} from "../app/accounts/browse/BrowseAccounts";

export const routes: RouteObject[] = [
  {
    path: '/',
    element: <App />,
    children: [
      {path: '/login', element: <SignInPage />},
      {path: '/register', element: <SignUpPage />},

      {path: '/home', element: <HomePage />},

      {path: '/budget-plans', element: <BudgetPlansPage />},

      {path: '/accounts', element: <AccountsPage />},
      {path: '/accounts/new', element: <AddAccount />},
      {path: '/accounts/browse', element: <BrowseAccounts />},

      {path: '/goals', element: <GoalsPage />},

      {path: '/statistics', element: <StatisticsPage  />},

      {path: '/settings', element: <SettingsPage  />},

      {path: '/user', element: <UserPage />},

      {path: '*', element: <NotFoundPage text={'We are sorry, the content you are looking for does not exist 😔'} />}
    ]
  }
]

export const router = createBrowserRouter(routes);