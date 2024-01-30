import AppOverlay, {dashboardSections} from "../../../components/appOverlay/AppOverlay";
import {useStore} from "../../../stores/store";
import {observer} from "mobx-react-lite";
import {Button, ButtonGroup, Grid, IconButton, Stack, Typography, useMediaQuery} from "@mui/material";
import theme from "../../theme";
import {Form, Formik} from "formik";
import MyTextInput from "../../../components/MyTextInput";
import {UndoTwoTone} from "@mui/icons-material";
import {useNavigate} from "react-router-dom";
import {NewAccountData} from "../../../models/requests/newAccountData";


export default observer(function AddAccount() {
    const navigate = useNavigate()
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
    const isUwhd = useMediaQuery(theme.breakpoints.up('xl'))
    const {accountStore} = useStore()

    //const validationSchema //TODO Implement validation schema

    const handleFormSubmit = async (accountData: NewAccountData) => {
        await accountStore.addAccount(accountData)
    }

    return (
        <AppOverlay>
            <Grid container sx={{
                height:'100%',
                padding: isMobile ? 3 : 4,
                margin: 0,
                backgroundColor: theme.palette.background.paper,
                borderRadius: '20px',
                overflow:'auto',
                display:'flex',
                justifyContent: 'center',
                alignItems: 'center'
            }}>
                <Grid item xs={12} sx={{
                    height: '65%',
                    display:'flex',
                    justifyContent: 'center',
                    alignItems: 'center'
                }}>
                    <Formik initialValues={new NewAccountData()} onSubmit={handleFormSubmit}>
                        {({handleSubmit, setValues, values}) => (
                            <Form style={{width: isUwhd ? '15%' : '40%'}} className='ui form' onSubmit={handleSubmit} autoComplete='off'>
                                <Stack spacing={3} sx={{textAlign:'center'}}>
                                    {!isMobile &&
                                        <Typography variant={'h3'} sx={{userSelect: 'none', paddingBottom: '3%'}}>
                                            Add new account
                                        </Typography>
                                    }
                                    <MyTextInput
                                        name={'type'}
                                        placeholder={'Account Type'}
                                    />
                                    <MyTextInput
                                        name={'name'}
                                        placeholder={'Account Name'}
                                    />
                                    <MyTextInput
                                        name={'initialBalance'}
                                        placeholder={'Initial Account Balance'}
                                    />
                                    <ButtonGroup sx={{alignItems:'center', justifyContent: 'center'}}>
                                        <Button fullWidth
                                                variant={'outlined'}
                                                color={'error'}
                                                onClick={() => navigate('/accounts')}
                                        >
                                            <UndoTwoTone color={'error'} />
                                        </Button>
                                        <Button fullWidth variant={'contained'} type={'submit'}>
                                            Add
                                        </Button>
                                    </ButtonGroup>
                                </Stack>
                            </Form>
                        )}
                    </Formik>
                </Grid>
            </Grid>
        </AppOverlay>
    );
})