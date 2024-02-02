import {Account} from "../../../models/accounts/account";
import {Box, Button, ButtonGroup, Grid, Stack, Typography, useMediaQuery} from "@mui/material";
import {Form, Formik} from "formik";
import * as Yup from 'yup'
import {AccountUpdateData} from "../../../models/requests/accountUpdateData";
import {number, string} from "yup";
import {ReactNode, useEffect, useState} from "react";
import MyTextInput from "../../../components/MyTextInput";
import theme from "../../theme";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../stores/store";
import GroupDropdown, {GroupDropdownProps} from "./GroupDropdown";
import {toast} from "react-toastify";
import {useNavigate} from "react-router-dom";
import useAccount from "../../../utils/hooks/useAccount";
import {isVisible} from "@testing-library/user-event/dist/utils";
import {runInAction} from "mobx";

interface EditAccountComponentProps {
    account: Account
    groupDropdownProps: GroupDropdownProps
    setAccount: (account: Account) => void
}

export default observer(function EditAccountComponent({ account, groupDropdownProps, setAccount }: EditAccountComponentProps) {
    const initialValues = new AccountUpdateData(account)
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
    const {accountStore} = useStore()
    const navigate = useNavigate()

    const validationSchema = Yup.object({
        name: string()
            .required('Pass account name')
            .max(30, 'Account name should be shorter than 30 characters')
            .matches(RegExp('^[a-zA-Z0-9\\s]*$'), {message: "Special characters are not allowed in account name"}),
        balance: number().required('Balance is required')
    })

    const leaveEditMode = () => navigate(`/accounts/${account.accountId}`)

    const updateAccount = async (values: AccountUpdateData) => await accountStore.updateAccount(values)

    const handleFormSubmit = (values: AccountUpdateData) => {
        updateAccount(values).then(a => {
            console.log(a)
            a && setAccount(a)
            leaveEditMode()
        })
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
                            flexDirection: 'column'
                    }}>
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
                                        onClick={leaveEditMode}
                                    >
                                        Cancel
                                    </Button>
                                    <Button
                                        disabled={!isValid || initialValues === values}
                                        sx={{width: '50%'}}
                                        variant="contained"
                                        color="success"
                                        type={'submit'}
                                    >
                                        Accept
                                    </Button>
                                </ButtonGroup>
                            </Grid>
                            <GroupDropdown groupCriterion={groupDropdownProps.groupCriterion} handleGroupChange={groupDropdownProps.handleGroupChange} />
                        </Grid>
                    </Form>
                )}
            </Formik>
        </>
    );
})