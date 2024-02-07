import {Account} from "../../../../models/accounts/account";
import {
    Button,
    ButtonGroup,
    Grid, IconButton, InputAdornment,
    Stack,
    Typography,
    useMediaQuery
} from "@mui/material";
import {Form, Formik} from "formik";
import * as Yup from 'yup'
import {AccountUpdateData} from "../../../../models/requests/accountUpdateData";
import {number, string} from "yup";
import MyTextInput from "../../../../components/MyTextInput";
import theme from "../../../theme";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../../stores/store";
import GroupDropdown, {GroupDropdownProps} from "../transactionsTable/GroupDropdown";
import {useNavigate} from "react-router-dom";
import React from "react";
import {toast} from "react-toastify";
import {DeleteTwoTone, Undo} from "@mui/icons-material";
import ConfirmModal from "../../../../components/ConfirmModal";

interface EditAccountComponentProps {
    account: Account
    groupDropdownProps: GroupDropdownProps
    setAccount: (account: Account | undefined) => void
    setEditMode: (state: boolean) => void
}

export default observer(function EditAccountComponent({ account, groupDropdownProps, setEditMode, setAccount}: EditAccountComponentProps) {
    const initialValues = new AccountUpdateData(account)
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
    const {accountStore, transactionStore, modalStore} = useStore()
    const navigate = useNavigate()

    const validationSchema = Yup.object({
        name: string()
            .required('Pass account name')
            .max(30, 'Account name should be shorter than 30 characters')
            .matches(RegExp('^[a-zA-Z0-9][a-zA-Z0-9\\s]*[a-zA-Z0-9]$'), {message: "Special characters are not allowed in account name"}),
        balance: number().required('Balance is required')
    })

    const updateAccount = async (values: AccountUpdateData) => await accountStore.updateAccount(values)

    const removeTransactions = async () => await transactionStore.removeTransactions()

    const handleFormSubmit = (values: AccountUpdateData) => {
        if(initialValues === values && transactionStore.transactionsIdToRemove.length === 0) {
            toast.warn('Nothing to update')
            setEditMode(false)
            return
        }
        if(transactionStore.transactionsIdToRemove.length > 0) {
            modalStore.openModal(
                <ConfirmModal text={`You are about to delete ${transactionStore.transactionsIdToRemove.length} transactions from the account ${account.name}. Are you sure you want to proceed?`}
                              onConfirm={ () =>
                                  removeTransactions().then(() => {
                                      accountStore.loadAccounts().then(() => setAccount(accountStore.getAccount(values.accountId)))
                                      setEditMode && setEditMode(false)
                                  })
                              }
                              important
                />
            )
        }
        initialValues !== values && updateAccount(values).then(a => {
            setAccount(a)
            setEditMode && setEditMode(false)
        })
    }

    function handleDelete() {
        modalStore.openModal(
            <ConfirmModal text={`You are about to delete ${account.name}. Are you sure you want to proceed?`}
                          onConfirm={ () =>
                              accountStore.deleteAccount(account.accountId).then(() =>
                                  navigate('/accounts')
                              )
                          }
                          important
            />
        )
    }

    return (
        <>
            <Formik sx={{width: '100%'}} initialValues={initialValues} onSubmit={handleFormSubmit} validationSchema={validationSchema}>
                {({setValues, values, isValid}) => (
                    <Form autoComplete='off'
                          style={{
                            width: '100%',
                            display: 'flex',
                            justifyContent: 'center',
                            alignItems: 'center',
                            flexDirection: 'column',
                            position: 'relative'
                    }}>
                        <IconButton onClick={() => navigate('/accounts')}
                            sx={{
                                position: 'absolute',
                                top: '1px', left: '1px',
                                width: '5rem',
                                height: '5rem'
                        }}>
                            <Undo fontSize={'large'} />
                        </IconButton>
                        <IconButton onClick={handleDelete}
                            sx={{
                                position: 'absolute',
                                top: '1px',
                                right: '1px',
                                width: '5rem',
                                height: '5rem',
                                flexDirection: 'column',
                                color: theme.palette.error.main,
                        }}>
                            <DeleteTwoTone fontSize={'large'} />
                            <Typography variant={'caption'}>Delete</Typography>
                        </IconButton>
                        <Grid container>
                            <Grid item xs={12} marginBottom={3} sx={{display: 'flex', alignItems:'center', justifyContent: 'center'}}>
                                <Stack sx={{justifyContent: 'center', alignItems: 'center', width:'100%'}}>
                                    <Typography variant={"subtitle1"} sx={{ userSelect: "none" }}>
                                        Account Name
                                    </Typography>
                                    <MyTextInput
                                        style={{width: '60%', maxWidth: '500px'}}
                                        placeholder={''}
                                        name={'name'}
                                        type={'text'}
                                    />
                                </Stack>
                            </Grid>
                            <Grid item xs={12} marginBottom={3} sx={{display: 'flex', alignItems:'center', justifyContent: 'center'}}>
                                <Stack sx={{justifyContent: 'center', alignItems: 'center'}}>
                                    <Typography variant={"subtitle1"} sx={{ userSelect: "none" }}>
                                        Account Type
                                    </Typography>
                                    <Typography variant={"h4"} sx={{ userSelect: "none" }}>
                                        {account.accountType}
                                    </Typography>
                                </Stack>
                            </Grid>
                            <Grid item xs={12} marginBottom={3} sx={{display: 'flex', alignItems:'center', justifyContent: 'center'}}>
                                <Stack sx={{justifyContent: 'center', alignItems: 'center', width: '100%'}}>
                                    <Typography variant={"subtitle1"} sx={{ userSelect: "none" }}>
                                        Account Balance
                                    </Typography>
                                    <MyTextInput
                                        style={{width: '60%', maxWidth: '500px'}}
                                        placeholder={''}
                                        name={'balance'}
                                        type={'number'}
                                        inputProps={{endAdornment: <InputAdornment position="end">{account.balance.currency}</InputAdornment>}}
                                    />
                                </Stack>
                            </Grid>
                            <Grid item xs={12} md={6} marginBottom={isMobile ? 3 : 0} sx={{
                                display: 'flex',
                                justifyContent: 'center',
                                alignItems: 'center'
                            }}>
                                <ButtonGroup sx={{width: '60%', height: '3rem', maxWidth: '500px'}}>
                                    <Button
                                        sx={{width: '50%'}}
                                        variant="outlined"
                                        color='error'
                                        onClick={() => {
                                            transactionStore.clearTransactionIdToRemove()
                                            setEditMode(false)
                                        }}
                                    >
                                        Cancel
                                    </Button>
                                    <Button
                                        sx={{width: '50%'}}
                                        variant="contained"
                                        color="success"
                                        type={'submit'}
                                    >
                                        Accept
                                    </Button>
                                </ButtonGroup>
                            </Grid>
                            <Grid item xs={12} md={6} marginBottom={3} sx={{display: 'flex', alignItems:'center', justifyContent: 'center'}}>
                                <GroupDropdown groupCriterion={groupDropdownProps.groupCriterion} handleGroupChange={groupDropdownProps.handleGroupChange} />
                            </Grid>
                        </Grid>
                    </Form>
                )}
            </Formik>
        </>
    );
})