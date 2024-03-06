import AppOverlay from "../../components/appOverlay/AppOverlay";
import {TilesLayout} from "../../components/tilesLayout/TilesLayout";
import {observer} from "mobx-react-lite";
import useTitle from "../../utils/hooks/useTitle";
import theme from "../theme";
import {Button, Divider, Grid, Typography} from "@mui/material";
import {useStore} from "../../stores/store";
import {Logout} from "@mui/icons-material";
import {useNavigate} from "react-router-dom";

export default observer(function UserPage() {
    const {userStore} = useStore()
    const navigate = useNavigate()
    useTitle(undefined, `Username`)

    function handleLogOut() {
        userStore.logOut()
        navigate('/logout')
    }

    return (
        <AppOverlay>
            <Grid container  sx={{
                height:'100%',
                padding: 5,
                margin: 0,
                backgroundColor: theme.palette.background.paper,
                borderRadius: '20px',
                overflowY:'auto',
                overflowX:'hidden',
                maxWidth: '1920px',
                userSelect: 'none',
                display: 'flex', alignItems: 'center', justifyContent: 'center'
            }}>
                <Grid item xs={12} textAlign={'center'}>
                    <Typography variant={'h3'}>
                        User personal info
                    </Typography>
                </Grid>
                <Grid item xs={12} sx={{
                    display: 'flex', justifyContent: 'center', alignItems: 'center',flexDirection: 'column'
                }}>
                    <Typography variant={'h3'} marginBottom={2}>
                        Email
                    </Typography>
                    <Typography marginBottom={2} width={'300px'}>
                        <Divider variant={'middle'} />
                    </Typography>
                    <Typography variant={'h5'}>
                        {userStore.currentUser?.email}
                    </Typography>
                </Grid>
                <Grid item xs={12} sx={{
                    display: 'flex', justifyContent: 'center', alignItems: 'center',flexDirection: 'column'
                }}>
                    <Typography variant={'h3'} marginBottom={2}>
                        Currency
                    </Typography>
                    <Typography marginBottom={2} width={'300px'}>
                        <Divider variant={'middle'} />
                    </Typography>
                    <Typography variant={'h5'}>
                        {userStore.currentUser?.currency}
                    </Typography>
                </Grid>
                <Grid item xs={12} sx={{
                    display: 'flex', alignItems: 'center', justifyContent: 'center'
                }}>
                    <Button variant={'outlined'} onClick={() => handleLogOut()} color={'error'} sx={{
                        padding: theme.spacing(5),
                        flexDirection: 'column',
                        width: '200px',
                        height: '200px'
                    }}>
                        <Logout sx={{fontSize: 70}} />
                        <Typography variant={'caption'}>
                            Log out
                        </Typography>
                    </Button>
                </Grid>
            </Grid>
        </AppOverlay>
    );
})