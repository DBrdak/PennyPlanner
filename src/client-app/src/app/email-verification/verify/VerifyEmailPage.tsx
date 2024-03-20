import {useLocation, useNavigate, useParams} from "react-router-dom";
import {useEffect} from "react";
import {useStore} from "../../../stores/store";

export function VerifyEmailPage() {
    const {userStore} = useStore()
    const navigate = useNavigate()
    const location = useLocation()

    const getEmailFromParams = () =>
        location.search.slice(location.search.indexOf('email=') + 'email='.length)

    const getTokenFromParams = () =>
        location.search.slice(location.search.indexOf('token=') + 'token='.length, location.search.indexOf('email=') - 1)

    useEffect(() => {
        const email = getEmailFromParams()
        const token = getTokenFromParams()

        email && token && userStore.verifyEmail(email, token)
            .then(async () => {
                await userStore.loadCurrentUser()

                navigate('/home')
            })
    }, [])

    return (
        <></>
    );
}