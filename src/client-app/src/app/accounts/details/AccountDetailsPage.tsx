import {useParams} from "react-router-dom";
import AppOverlay from "../../../components/appOverlay/AppOverlay";
import {useStore} from "../../../stores/store";
import {observer} from "mobx-react-lite";
import {CircularProgress, Grid, Paper, Typography, useMediaQuery} from "@mui/material";
import theme from "../../theme";
import {useEffect, useState} from "react";
import {Account} from "../../../models/accounts/account";
import {runInAction} from "mobx";

const useAccount = () => {
    const { accountStore } = useStore();
    const { accountId } = useParams<{ accountId: string }>();
    const [account, setAccount] = useState<Account | undefined>();

    useEffect(() => {
        const loadAccounts = async () => {
            try {
                await accountStore.loadAccounts();
            } catch (error) {
                console.error('Error loading accounts:', error);
            }
        };

        const findAccount = () => accountStore.accounts.find(a => a.accountId === accountId);

        if (accountStore.accounts.length < 1) {
            loadAccounts().then(() => setAccount(findAccount()));
        } else {
            setAccount(findAccount());
        }
    }, [accountStore, accountId]);

    return account;
};

export default observer(function AccountDetailsPage() {
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
    const account = useAccount();

    return (
        <AppOverlay>
            {!account ? <CircularProgress color={'secondary'} /> :
                <Grid container sx={{
                    height:'100%',
                    padding: isMobile ? 3 : 4,
                    margin: 0,
                    backgroundColor: theme.palette.background.paper,
                    borderRadius: '20px',
                    overflow:'auto'
                }}>
                    <Grid item xs={12} sx={{textAlign: 'center'}}>
                        <Typography variant={'h4'} sx={{userSelect: 'none'}}>
                            {account.name}
                        </Typography>
                    </Grid>
                </Grid>
            }
        </AppOverlay>
    );
})