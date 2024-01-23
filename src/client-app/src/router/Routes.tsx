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
      {path: '/dashboard', element: <DashboardPage />},
      {path: '*', element: <NotFoundPage text={'Nie znaleźliśmy szukanej zawarości 😔'} />}
    ]
  }
]

export const router = createBrowserRouter(routes);