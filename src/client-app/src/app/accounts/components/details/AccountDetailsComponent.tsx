import {Button, Grid, Stack, Typography} from "@mui/material";
import AccountDetailsPage from "../../details/AccountDetailsPage";
import {Account} from "../../../../models/accounts/account";
import GroupDropdown, {GroupDropdownProps} from "../transactionsTable/GroupDropdown";
import {observer} from "mobx-react-lite";


interface AccountDetailsComponentProps {
    account: Account
    groupDropdownProps: GroupDropdownProps
    setEditMode: (state: boolean) => void
}


export default observer(function AccountDetailsComponent({account, groupDropdownProps,setEditMode}: AccountDetailsComponentProps) {

    return (
        <>
            <Grid item xs={12} sx={{display: 'flex', alignItems:'center', justifyContent: 'center'}}>
                <Stack sx={{justifyContent: 'center', alignItems: 'center'}}>
                    <Typography variant={"subtitle1"} sx={{ userSelect: "none" }}>
                        Account Name
                    </Typography>
                    <Typography variant={"h4"} sx={{ userSelect: "none" }}>
                        {account.name}
                    </Typography>
                </Stack>
            </Grid>
            <Grid item xs={12} sx={{display: 'flex', alignItems:'center', justifyContent: 'center'}}>
                <Stack sx={{justifyContent: 'center', alignItems: 'center'}}>
                    <Typography variant={"subtitle1"} sx={{ userSelect: "none" }}>
                        Account Type
                    </Typography>
                    <Typography variant={"h4"} sx={{ userSelect: "none" }}>
                        {account.accountType}
                    </Typography>
                </Stack>
            </Grid>
            <Grid item xs={12} sx={{display: 'flex', alignItems:'center', justifyContent: 'center'}}>
                <Stack sx={{justifyContent: 'center', alignItems: 'center'}}>
                    <Typography variant={"subtitle1"} sx={{ userSelect: "none" }}>
                        Account Balance
                    </Typography>
                    <Typography variant={"h4"} sx={{ userSelect: "none" }}>
                        {account.balance.amount.toFixed(2)} {account.balance.currency}
                    </Typography>
                </Stack>
            </Grid>
            <Grid item xs={12} md={6} marginBottom={3} sx={{display: 'flex', alignItems:'center', justifyContent: 'center'}}>
                <Button
                    variant="contained"
                    color="primary"
                    sx={{ width: '60%', maxWidth: '500px', height: '3rem'}}
                    onClick={() => setEditMode(true)}
                >
                    Edit Account
                </Button>
            </Grid>
            <Grid item xs={12} md={6} marginBottom={3} sx={{display: 'flex', alignItems:'center', justifyContent: 'center'}}>
                <GroupDropdown groupCriterion={groupDropdownProps.groupCriterion} handleGroupChange={groupDropdownProps.handleGroupChange} />
            </Grid>
        </>
    );
})