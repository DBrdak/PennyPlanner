import { Navigate, Outlet, useLocation } from 'react-router-dom'
import { useStore } from '../stores/store'
import {toast} from "react-toastify";

function RequireAuth() {
  const {userStore} = useStore()
  const location = useLocation()

  if (!userStore.currentUser && !userStore.token){
    toast.warn('Please log in')
    return <Navigate to={'/'} state={{from: location}}/>
  }

  return <Outlet />
}

export default RequireAuth