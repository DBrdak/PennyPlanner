 import React from 'react';
import {
    Toolbar,
    List,
    ListItemIcon,
    ListItemText,
    IconButton,
    Avatar,
    Typography, Box,
} from '@mui/material';
import {
    AccountBalanceTwoTone, AccountBoxTwoTone, AssessmentTwoTone, CalendarMonthTwoTone,
    ChevronLeftTwoTone,
    ChevronRightTwoTone, EditNoteTwoTone, EmojiEventsTwoTone, HomeTwoTone,
} from "@mui/icons-material";
import Drawer, {drawerWidth} from "./Drawer";
import AppBar from "./AppBar";
import theme from "../../app/theme";
import '../../styles/index.css'
import DrawerNavButton from "./DrawerNavButton";
import DrawerListItem from "./DrawerListItem";
import DrawerButton from "./DrawerButton";
import {useNavigate} from "react-router-dom";
import {observer} from "mobx-react-lite";
import {useStore} from "../../stores/store";
import DashboardSection from "../../models/layout/dashboardSection";

interface AppOverlayProps {
    children: React.ReactNode,
}

export const dashboardSections: DashboardSection[] = [
    {
        title: 'Home',
        icon: <HomeTwoTone color={'primary'} fontSize={'large'} />,
        activeIcon: <HomeTwoTone sx={{color: theme.palette.secondary.main}} fontSize={'large'} />
    },
    {
        title: 'Budget Plans',
        icon: <CalendarMonthTwoTone color={'primary'} fontSize={'large'} />,
        activeIcon: <CalendarMonthTwoTone sx={{color: theme.palette.secondary.main}} fontSize={'large'} />
    },
    {
        title: 'Accounts',
        icon: <AccountBalanceTwoTone color={'primary'} fontSize={'large'} />,
        activeIcon: <AccountBalanceTwoTone sx={{color: theme.palette.secondary.main}} fontSize={'large'} />
    },
    {
        title: 'Goals',
        icon: <EmojiEventsTwoTone color={'primary'} fontSize={'large'} />,
        activeIcon: <EmojiEventsTwoTone sx={{color: theme.palette.secondary.main}} fontSize={'large'} />
    },
    {
        title: 'Statistics',
        icon: <AssessmentTwoTone color={'primary'} fontSize={'large'} />,
        activeIcon: <AssessmentTwoTone sx={{color: theme.palette.secondary.main}} fontSize={'large'} />
    },
    {
        title: 'Settings',
        icon: <EditNoteTwoTone color={'primary'} fontSize={'large'} />,
        activeIcon: <EditNoteTwoTone sx={{color: theme.palette.secondary.main}} fontSize={'large'} />
    },
    {
        title: 'User',
        icon: <AccountBoxTwoTone color={'primary'} fontSize={'large'} />,
        activeIcon: <AccountBoxTwoTone sx={{color: theme.palette.secondary.main}} fontSize={'large'} />,
        isNotSideNavBar: true
    },
]

const AppOverlay = ({children}: AppOverlayProps) => {
    const {layoutStore} = useStore();
    const navigate = useNavigate();

    const handleDrawerClick = () => {
        layoutStore.setDrawerState();
    };

    function handleSectionChange(index: number) {
        layoutStore.setActiveSectionIndex(index)
        const section = dashboardSections[index].title;
        const sectionPath = section.replace(' ', '-').toLowerCase();
        navigate(`/${sectionPath}`);
    }

    const isActiveSection = (index: number) => layoutStore.activeSectionIndex === index;

    const sideNavBarSections = dashboardSections.filter(s => !s.isNotSideNavBar)

    const userSectionIndex = dashboardSections.findIndex(s => s.title === 'User')

    return (
        <Box sx={{height: '100svh', overflow: 'hidden'}}>
            <AppBar>
                <Toolbar style={{
                    height: '100%',
                    alignItems: 'center',
                    justifyContent: 'space-between',
                    padding: `0px 10px`,
                    backgroundColor: theme.palette.background.paper
                }}>
                    <DrawerNavButton
                        disableRipple
                        isOpen={layoutStore.isDrawerOpen}
                        color="inherit"
                        aria-label="open drawer"
                        onClick={handleDrawerClick}
                        edge="start"
                    >
                        {!layoutStore.isDrawerOpen ?
                            <ChevronRightTwoTone color={'primary'} /> :
                            <ChevronLeftTwoTone color={'primary'} />}
                    </DrawerNavButton>
                    <Typography variant={'h3'} style={{
                        userSelect: 'none',
                        fontFamily: 'Fira Sans',
                        color: theme.palette.primary.light,
                        fontWeight: 'bold'
                    }}>
                        Budgetify
                    </Typography>
                    <IconButton sx={{
                        borderRadius: 0,
                        height: '100%',
                        aspectRatio: 1,
                        padding: 'none',
                        margin: 'none',
                        ":disabled": {backgroundColor: theme.palette.background.default},
                        backgroundColor: 'transparent'
                    }}
                                onClick={() => handleSectionChange(sideNavBarSections.length)}
                                disabled={isActiveSection(userSectionIndex)}
                    >
                        <Avatar sx={{
                            borderRadius: 0,
                            aspectRatio: 1,
                            padding: 'none',
                            backgroundColor: 'transparent'
                        }}>
                            {isActiveSection(userSectionIndex) ?
                                dashboardSections[userSectionIndex].activeIcon :
                                dashboardSections[userSectionIndex].icon}
                        </Avatar>
                    </IconButton>
                </Toolbar>
            </AppBar>
            <Box style={{
                display: 'flex',
                height: `calc(100% - ${theme.spacing(8)} + 1px)`
            }}>
                <Drawer
                    open={layoutStore.isDrawerOpen}
                    variant={'permanent'}
                    anchor="left"
                >
                    <List style={{
                        margin: '0',
                        padding: '0',
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                        justifyContent: 'center',
                        height: '100%',
                        width: '100%'
                    }}>
                        {sideNavBarSections.map((btn, index) => (
                            <DrawerListItem key={index}
                                            onClick={() => handleSectionChange(index)}
                                            isactive={isActiveSection(index).toString()}
                                            itemscount={sideNavBarSections.length}>
                                <DrawerButton
                                    disabled={isActiveSection(index)}
                                >
                                    <ListItemIcon sx={{
                                        minWidth: '0px',
                                        display: 'flex',
                                        justifyContent: 'center',
                                        alignItems: 'center',
                                        textAlign: 'center'
                                    }}>
                                        {isActiveSection(index) ? btn.activeIcon : btn.icon}
                                    </ListItemIcon>
                                    {layoutStore.isDrawerOpen && (<ListItemText primary={btn.title} primaryTypographyProps={{
                                        fontSize: '1.3rem',
                                        fontWeight: '700',
                                        color: isActiveSection(index) ? theme.palette.secondary.light : theme.palette.primary.light,
                                        letterSpacing: 0,
                                    }}/>)}
                                </DrawerButton>
                            </DrawerListItem>
                        ))}
                    </List>
                </Drawer>
                <Box
                    component="main"
                    sx={{ flexGrow: 1,
                        width: {sm: `calc(100% - ${drawerWidth}px)` },
                        padding: '3%',
                        justifyContent: 'center',
                        alignItems: 'center',
                        display: 'flex' }}
                >
                    {children}
                </Box>
            </Box>
        </Box>
    );
};

export default observer(AppOverlay);
