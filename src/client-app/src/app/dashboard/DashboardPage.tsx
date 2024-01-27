import AppOverlay from "../../components/appOverlay/AppOverlay";
import React from "react";
import {
    AccountBalanceTwoTone, AccountBoxTwoTone,
    AssessmentTwoTone,
    CalendarMonthTwoTone, EditNoteTwoTone,
    EmojiEventsTwoTone,
    HomeTwoTone
} from "@mui/icons-material";

export const dashboardSections: {title: string, icon: React.ReactNode}[] = [
    {title: 'Home', icon: <HomeTwoTone color={'secondary'} fontSize={'large'} />},
    {title: 'Budget Plans', icon: <CalendarMonthTwoTone color={'secondary'} fontSize={'large'} />},
    {title: 'Accounts', icon: <AccountBalanceTwoTone color={'secondary'} fontSize={'large'} />},
    {title: 'Goals', icon: <EmojiEventsTwoTone color={'secondary'} fontSize={'large'} />},
    {title: 'Statistics', icon: <AssessmentTwoTone color={'secondary'} fontSize={'large'} />},
    {title: 'Settings', icon: <EditNoteTwoTone color={'secondary'} fontSize={'large'} />},
    {title: 'User', icon: <AccountBoxTwoTone color={'secondary'} fontSize={'large'} />},
]

interface DashboardPageProps {
    activeSectionIndex: number
}
// TODO Export all repetitive data to store, and use store to display children of dashboard
export function DashboardPage({activeSectionIndex}: DashboardPageProps) {
    return (
        <AppOverlay activeSectionIndex={activeSectionIndex}>
            {/*layoutStore.activeSection*/}
        </AppOverlay>
    );
}