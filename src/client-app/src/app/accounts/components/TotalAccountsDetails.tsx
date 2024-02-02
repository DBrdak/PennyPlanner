import GroupDropdown, {GroupDropdownProps} from "./GroupDropdown";
import {Account} from "../../../models/accounts/account";
import {Grid} from "@mui/material";

interface TotalAccountsDetailsProps {
    account: Account
    groupDropdownProps: GroupDropdownProps
}

export function TotalAccountsDetails({account, groupDropdownProps}: TotalAccountsDetailsProps) {
    return (
        <>
            <Grid item xs={12} marginBottom={3} sx={{display: 'flex', alignItems:'center', justifyContent: 'center'}}>
                <GroupDropdown groupCriterion={groupDropdownProps.groupCriterion} handleGroupChange={groupDropdownProps.handleGroupChange} />
            </Grid>
        </>
    );
}