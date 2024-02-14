import {FormControl, InputLabel, MenuItem, Select} from "@mui/material";

export interface GroupDropdownProps{
    groupCriterion: string
    handleGroupChange: (group: string) => void
    noAccount?: boolean
}

export default function GroupDropdown({groupCriterion, handleGroupChange, noAccount}: GroupDropdownProps) {


    return (
        <FormControl sx={{width: '60%', maxWidth: '500px'}}>
            <InputLabel>Group By</InputLabel>
            <Select fullWidth value={groupCriterion}
                    onChange={(e) => handleGroupChange(e.target.value)}>
                <MenuItem key={1} value={'day'}>Day</MenuItem>
                <MenuItem key={2} value={'month'}>Month</MenuItem>
                <MenuItem key={3} value={'year'}>Year</MenuItem>
                <MenuItem key={4} value={'entity'}>Entity</MenuItem>
                <MenuItem key={5} value={'category'}>Category</MenuItem>
                <MenuItem key={6} value={'type'}>Type</MenuItem>
                {noAccount && <MenuItem key={7} value={'account'}>Account</MenuItem>}
            </Select>
        </FormControl>
    )
}