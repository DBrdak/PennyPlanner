import {useStore} from "../../stores/store";
import {useNavigate, useParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {Account} from "../../models/accounts/account";

const useAccount = () => {
    const { accountStore } = useStore();
    const { accountId } = useParams<{ accountId: string }>();
    const [account, setAccount] = useState<Account | undefined>();
    const navigate = useNavigate()

    useEffect(() => {
        const loadAccounts = async () => {
            await accountStore.loadAccounts();
        };

        const getAccount = (accountId: string) => accountStore.getAccount(accountId)

        if (accountStore.accounts.length < 1 && accountId) {
            loadAccounts().then(() => setAccount(accountStore.getAccount(accountId)));
        } else if(accountId && getAccount(accountId)) {
            setAccount(getAccount(accountId));
        } else {
            navigate('/not-found')
        }
    }, [accountStore, accountId, accountStore.loadAccounts]);

    return account;
};

export default useAccount