import { RouteObject } from "react-router"
import {createBrowserRouter} from 'react-router-dom'
import NotFoundPage from "../components/NotFoundPage";
import App from "../app/App";

export const routes: RouteObject[] = [
  {
    path: '/',
    element: <App />,
    children: [
      {path: '*', element: <NotFoundPage text={'Nie znaleźliśmy szukanej zawarości 😔'} />}
    ]
  }
]

export const router = createBrowserRouter(routes);