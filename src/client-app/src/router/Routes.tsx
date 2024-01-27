import { RouteObject } from "react-router"
import {createBrowserRouter} from 'react-router-dom'
import NotFoundPage from "../components/NotFoundPage";
import App from "../app/App";
import SignInPage from "../app/login/SignInPage";
import SignUpPage from "../app/register/SignUpPage";
import {DashboardPage} from "../app/dashboard/DashboardPage";

export const routes: RouteObject[] = [
  {
    path: '/',
    element: <App />,
    children: [
      {path: '/login', element: <SignInPage />},
      {path: '/register', element: <SignUpPage />},
      {path: '/home', element: <DashboardPage activeSectionIndex={0} />},
      {path: '/budget-plans', element: <DashboardPage activeSectionIndex={1} />},
      {path: '/accounts', element: <DashboardPage activeSectionIndex={2} />},
      {path: '/goals', element: <DashboardPage activeSectionIndex={3} />},
      {path: '/statistics', element: <DashboardPage activeSectionIndex={4} />},
      {path: '/settings', element: <DashboardPage activeSectionIndex={5} />},
      {path: '/user', element: <DashboardPage activeSectionIndex={6} />},
      {path: '*', element: <NotFoundPage text={'Nie znaleźliśmy szukanej zawarości 😔'} />}
    ]
  }
]

export const router = createBrowserRouter(routes);