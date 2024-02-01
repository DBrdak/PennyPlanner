import { RouteObject } from "react-router"
import {createBrowserRouter} from 'react-router-dom'
import NotFoundPage from "../components/NotFoundPage";
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
import {TotalAccountsDetailsPage} from "../app/accounts/details/TotalAccountsDetailsPage";

export const routes: RouteObject[] = [
  {
    path: '/',
    element: <App />,
    children: [
      {path: '', element: <WelcomePage />},
      {path: '/login', element: <SignInPage />},
      {path: '/register', element: <SignUpPage />},

      {path: '/home', element: <HomePage />},

      {path: '/budget-plans', element: <BudgetPlansPage />},

      {path: '/accounts', element: <AccountsPage />},
      {path: '/accounts/new', element: <AddAccountPage />},
      {path: '/accounts/:accountId', element: <AccountDetailsPage />},
      {path: '/accounts/total', element: <TotalAccountsDetailsPage />},

      {path: '/goals', element: <GoalsPage />},

      {path: '/statistics', element: <StatisticsPage  />},

      {path: '/settings', element: <SettingsPage  />},

      {path: '/user', element: <UserPage />},

      {path: '*', element: <NotFoundPage text={'We are sorry, the content you are looking for does not exist 😔'} />}
    ]
  }
]

export const router = createBrowserRouter(routes);