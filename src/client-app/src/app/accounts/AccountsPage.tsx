import AppOverlay, {dashboardSections} from "../../components/appOverlay/AppOverlay";
import {useLocation} from "react-router-dom";
import {Grid, Typography} from "@mui/material";
import {TilesLayout} from "../../components/TilesLayout";

export function AccountsPage() {
    const activeSectionIndex = dashboardSections.findIndex(s => s.title.toLowerCase() === 'accounts')

    return (
        <AppOverlay activeSectionIndex={activeSectionIndex}>
            <TilesLayout tiles={[
                {cols: 8, content:[
                    <Typography>VIEW ACCOUNTS</Typography>
                ]},
                {cols: 4, content:[
                    <Typography>VIEW ACCOUNTS</Typography>,
                    <Typography>VIEW ACCOUNTS</Typography>
                ]},
                {cols: 4, content:[
                        <Typography>VIEW ACCOUNTS</Typography>
                ]},
                {cols: 8, content:[
                    <Typography>VIEW ACCOUNTS</Typography>
                ]},
            ]} />
        </AppOverlay>
    );
}