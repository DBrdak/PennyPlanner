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
    ChevronLeft,
    ChevronRight, EditNoteTwoTone, EmojiEventsTwoTone, HomeTwoTone,
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
import {css} from "@mui/system";

interface AppOverlayProps {
    children: React.ReactNode,
    activeSectionIndex: number
}

export const dashboardSections: {title: string, icon: React.ReactNode, isNotSideNavBar?: boolean | null}[] = [
    {title: 'Home', icon: <HomeTwoTone color={'secondary'} fontSize={'large'} />},
    {title: 'Budget Plans', icon: <CalendarMonthTwoTone color={'secondary'} fontSize={'large'} />},
    {title: 'Accounts', icon: <AccountBalanceTwoTone color={'secondary'} fontSize={'large'} />},
    {title: 'Goals', icon: <EmojiEventsTwoTone color={'secondary'} fontSize={'large'} />},
    {title: 'Statistics', icon: <AssessmentTwoTone color={'secondary'} fontSize={'large'} />},
    {title: 'Settings', icon: <EditNoteTwoTone color={'secondary'} fontSize={'large'} />},
    {title: 'User', icon: <AccountBoxTwoTone color={'secondary'} fontSize={'large'} />, isNotSideNavBar: true},
]

const AppOverlay = ({children, activeSectionIndex}: AppOverlayProps) => {
    const {layoutStore} = useStore()
    const [activeSection, setActiveSection] = React.useState<number>(activeSectionIndex)
    const navigate = useNavigate()

    const sideNavBarSections = dashboardSections.filter(s => !s.isNotSideNavBar)

    const userSectionIndex = dashboardSections.findIndex(s => s.title === 'User')

    const handleDrawerClick = () => {
        layoutStore.setDrawerState()
    };

    function handleSectionChange(index: number) {
        setActiveSection(index)
        const section = dashboardSections[index].title

        const sectionPath = section.replace(' ', '-').toLowerCase()

        navigate(`/${sectionPath}` )
    }

    return (
        <Box sx={{height: '100svh', overflow: 'hidden'}}>
            <AppBar>
                <Toolbar style={{height: '100%', alignItems: 'center', justifyContent: 'space-between', padding:`0px 10px`,
                    backgroundColor: theme.palette.background.paper}}>
                    <DrawerNavButton
                        disableRipple
                        isOpen={layoutStore.isDrawerOpen}
                        color="inherit"
                        aria-label="open drawer"
                        onClick={handleDrawerClick}
                        edge="start"
                    >
                        {!layoutStore.isDrawerOpen ? <ChevronRight /> : <ChevronLeft />}
                    </DrawerNavButton>
                    <Typography variant={'h3'} style={{userSelect:'none', fontFamily: 'Fira Sans', color: '#8f8f8f', fontWeight: 'bold'}}>
                        Budgetify
                    </Typography>
                    <IconButton sx={{borderRadius: 0, height: '100%', aspectRatio: 1, padding:'none', margin:'none',
                        ":disabled":{backgroundColor: theme.palette.background.default}, backgroundColor: 'transparent'}}
                        onClick={() => handleSectionChange(sideNavBarSections.length)}
                        disabled={activeSectionIndex === userSectionIndex || activeSection === userSectionIndex}
                    >
                        <Avatar sx={{borderRadius: 0, aspectRatio: 1, padding:'none',
                            backgroundColor: 'transparent' }}>
                            {dashboardSections[userSectionIndex].icon}
                        </Avatar>
                    </IconButton>
                </Toolbar>
            </AppBar>
            <Box style={{ display: 'flex', height: `calc(100% - ${theme.spacing(8)} + 1px)` }}>
                <Drawer
                    open={layoutStore.isDrawerOpen}
                    variant={'permanent'}
                    anchor="left"
                >
                    <List style={{margin: '0', padding:'0',
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                        justifyContent: 'center',
                        height: '100%', width:'100%'}}>
                        {sideNavBarSections.map((btn, index) => (
                            <DrawerListItem key={index}
                                            onClick={() => handleSectionChange(index)}
                                            isactive={(activeSection === index).toString() || (activeSectionIndex === index).toString()}
                                            itemscount={sideNavBarSections.length}>
                                <DrawerButton
                                    disabled={activeSectionIndex === index || activeSection === index}
                                >
                                    <ListItemIcon
                                        sx={{
                                            minWidth: '0px',
                                            display:'flex',
                                            justifyContent: 'center',
                                            alignItems: 'center',
                                            textAlign: 'center',
                                        }}
                                    >
                                        {btn.icon}
                                    </ListItemIcon>
                                    {layoutStore.isDrawerOpen && (<ListItemText primary={btn.title}/>)}
                                </DrawerButton>
                            </DrawerListItem>
                        ))}
                    </List>
                </Drawer>
                <Box
                    component="main"
                    sx={{ flexGrow: 1, width: { sm: `calc(100% - ${drawerWidth}px)` }, padding:'3%', justifyContent: 'center', alignItems: 'center', display:'flex' }}
                >
                    {children}
                </Box>
            </Box>
        </Box>
    );
};

export default observer(AppOverlay);
