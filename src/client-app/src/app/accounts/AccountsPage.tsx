import AppOverlay, {dashboardSections} from "../../components/appOverlay/AppOverlay";
import {useLocation} from "react-router-dom";
import {Grid, Typography} from "@mui/material";
import {TilesLayout} from "../../components/TilesLayout";

export function AccountsPage() {
    const activeSectionIndex = dashboardSections.findIndex(s => s.title.toLowerCase() === 'accounts')

    return (
        <AppOverlay activeSectionIndex={activeSectionIndex}>
            <TilesLayout tiles={[
                {cols: 8, rows: 6, content:
                    <img src={'/assets/view_account.jpg'} style={{width: '100%', height: '100%'}} />
                },
                {cols: 4, rows: 6, content:
                    <Typography>VIEW ACCOUNTS</Typography>
                },
                {cols: 4, rows: 2, content:
                    <Typography>VIEW ACCOUNTS</Typography>
                },
                {cols: 8, rows: 2, content:
                    <Typography>VIEW ACCOUNTS</Typography>
                },
            ]} />
        </AppOverlay>
    );
}