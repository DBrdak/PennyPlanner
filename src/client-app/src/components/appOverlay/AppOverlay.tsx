 import React, {useEffect} from 'react';
import {
    Toolbar,
    List,
    ListItemIcon,
    ListItemText,
    IconButton,
    Avatar,
    Typography, Box, useMediaQuery, SwipeableDrawer,
} from '@mui/material';
import {
    AccountBalanceWalletTwoTone, AccountBoxTwoTone, AssessmentTwoTone, CalendarMonthTwoTone,
    ChevronLeftTwoTone,
    ChevronRightTwoTone, EditNoteTwoTone, EmojiEventsTwoTone, HomeTwoTone, KeyboardArrowDown, KeyboardArrowUp,
} from "@mui/icons-material";
import Drawer, {drawerWidth} from "./Drawer";
import AppBar from "./AppBar";
import theme from "../../app/theme";
import '../../styles/index.css'
import DrawerNavButton from "./DrawerNavButton";
import DrawerListItem from "./DrawerListItem";
import DrawerButton from "./DrawerButton";
import {useLocation, useNavigate} from "react-router-dom";
import {observer} from "mobx-react-lite";
import {useStore} from "../../stores/store";
import DashboardSection from "../../models/layout/dashboardSection";
 import AddTransactionButton from "./AddTransactionButton";
 import {router} from "../../router/Routes";

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
        icon: <AccountBalanceWalletTwoTone color={'primary'} fontSize={'large'} />,
        activeIcon: <AccountBalanceWalletTwoTone sx={{color: theme.palette.secondary.main}} fontSize={'large'} />
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
    const currentLocation = useLocation()
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))

    useEffect(() => {
        layoutStore.setActiveSectionIndexByPath(currentLocation.pathname)
    }, [currentLocation, layoutStore])

    const handleDrawerClick = () => {
        layoutStore.setDrawerState();
    };

    function handleSectionChange(index: number) {
        layoutStore.setActiveSectionIndex(index)
        const sectionTitle = dashboardSections[index].title;
        const sectionPath = sectionTitle.replace(' ', '-').toLowerCase();
        navigate(`/${sectionPath}`);
    }

    const isActiveSection = (index: number) => layoutStore.activeSectionIndex === index;

    const sideNavBarSections = dashboardSections.filter(s => !s.isNotSideNavBar)

    const userSectionIndex = dashboardSections.findIndex(s => s.title === 'User')

    return (
        <Box sx={{height: '100svh', overflow: 'hidden', position: 'relative'}}>
            <AppBar>
                <Toolbar style={{
                    height: '100%',
                    alignItems: 'center',
                    justifyContent: 'space-between',
                    padding: `0px 10px`,
                    backgroundColor: theme.palette.background.paper
                }}>
                    {
                        !isMobile ?
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
                            :
                            <DrawerNavButton
                                disableRipple
                                color="inherit"
                                aria-label="open drawer"
                                onClick={handleDrawerClick}
                                edge="start"
                            >
                                {!layoutStore.isDrawerOpen ?
                                    <KeyboardArrowDown color={'primary'} /> :
                                    <KeyboardArrowUp color={'primary'} />}
                            </DrawerNavButton>
                    }
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
            {
                !isMobile ?
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
                                padding: '1.5% 3%',
                                justifyContent: 'center',
                                alignItems: 'center',
                                display: 'flex'
                            }}>
                            {children}
                        </Box>
                    </Box>
                    :
                    <Box style={{
                        display: 'flex',
                    }}>
                        <SwipeableDrawer
                            open={layoutStore.isDrawerOpen}
                            variant={'temporary'}
                            anchor="top"
                            onClose={() => layoutStore.setDrawerState()}
                            onOpen={() => layoutStore.setDrawerState()}
                        >
                            <List style={{
                                marginTop: `calc(${theme.spacing(8)} + 1px)`,
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
                                            sx={{flexDirection: 'column'}}
                                        >
                                            <ListItemIcon sx={{
                                                minWidth: '0px',
                                                display: 'flex',
                                                justifyContent: 'center',
                                                alignItems: 'center',
                                                textAlign: 'center',
                                                minHeight: '100px'
                                            }}>
                                                {isActiveSection(index) ? btn.activeIcon : btn.icon}
                                            </ListItemIcon>
                                            <ListItemText primary={btn.title} primaryTypographyProps={{
                                                fontSize: '1.3rem',
                                                fontWeight: '700',
                                                color: isActiveSection(index) ? theme.palette.secondary.light : theme.palette.primary.light,
                                                letterSpacing: 0,
                                            }}
                                            />
                                        </DrawerButton>
                                    </DrawerListItem>
                                ))}
                            </List>
                        </SwipeableDrawer>
                        <Box
                            component="main"
                            sx={{
                                flexGrow: 1,
                                width: '100%',
                                padding: 2,
                                justifyContent: 'center',
                                alignItems: 'center',
                                display: 'flex',
                                overflow: 'hidden',
                                height: '90vh'
                            }}>
                            {children}
                        </Box>
                    </Box>
            }
            <AddTransactionButton />
        </Box>
    );
};

export default observer(AppOverlay);
