import {useNavigate} from 'react-router-dom'
import { useStore } from '../../stores/store'
import {toast} from "react-toastify";
import {useEffect} from "react";


const useAuthProvider = () => {
    const {userStore} = useStore()
    const navigate = useNavigate()

    useEffect(() => {
        if (!userStore.currentUser && !userStore.token){
            toast.warn('Please log in')
            navigate('/')
        }

        if(userStore.currentUser && !userStore.currentUser.isEmailVerified) {
            navigate('/email-verification')
        }
    }, [navigate, userStore.currentUser, userStore.token])
}

export default useAuthProvider