import React from "react";

export default interface DashboardSection{
    title: string
    icon: React.ReactNode
    activeIcon: React.ReactNode
    isNotSideNavBar?: boolean | null
}