import React from 'react';
import {
    Toolbar,
    List,
    ListItemIcon,
    ListItemText,
    IconButton,
    Avatar,
    Typography,
} from '@mui/material';
import {
    ChevronLeft,
    ChevronRight,
} from "@mui/icons-material";
import Drawer from "./Drawer";
import AppBar from "./AppBar";
import theme from "../../app/theme";
import '../../styles/index.css'
import DrawerNavButton from "./DrawerNavButton";
import DrawerListItem from "./DrawerListItem";
import DrawerButton from "./DrawerButton";
import {dashboardSections} from "../../app/dashboard/DashboardPage";
import {useNavigate} from "react-router-dom";

interface AppOverlayProps {
    children: React.ReactNode,
    activeSectionIndex: number
}

const AppOverlay: React.FC<AppOverlayProps> = ({ children, activeSectionIndex }) => {
    const [drawerOpen, setDrawerOpen] = React.useState(false);
    const navigate = useNavigate()

    const sideNavBarSections = dashboardSections.filter(s => s.title !== 'User')

    const userSectionIndex = dashboardSections.findIndex(s => s.title === 'User')

    const handleDrawerClick = () => {
        setDrawerOpen(!drawerOpen)
    };

    function handleSectionChange(index: number) {
        const section = dashboardSections[index].title

        const sectionPath = section.replace(' ', '-').toLowerCase()

        navigate(`/${sectionPath}` )
    }

    return (
        <div>
            <AppBar>
                <Toolbar style={{height: '100%', alignItems: 'center', justifyContent: 'space-between', padding:`0px 10px`,
                    backgroundColor: theme.palette.background.paper}}>
                    <DrawerNavButton
                        disableRipple
                        isOpen={drawerOpen}
                        color="inherit"
                        aria-label="open drawer"
                        onClick={handleDrawerClick}
                        edge="start"
                    >
                        {!drawerOpen ? <ChevronRight /> : <ChevronLeft />}
                    </DrawerNavButton>
                    <Typography variant={'h3'} style={{userSelect:'none', fontFamily: 'Fira Sans', color: '#8f8f8f', fontWeight: 'bold'}}>
                        Budgetify
                    </Typography>
                    <IconButton sx={{borderRadius: 0, height: '100%', aspectRatio: 1, padding:'none', margin:'none',
                        ":disabled":{backgroundColor: theme.palette.background.default}, backgroundColor: 'transparent'}}
                        onClick={() => handleSectionChange(sideNavBarSections.length)}
                        disabled={activeSectionIndex === userSectionIndex}
                    >
                        <Avatar sx={{borderRadius: 0, aspectRatio: 1, padding:'none',
                            backgroundColor: 'transparent' }}>
                            {dashboardSections[userSectionIndex].icon}
                        </Avatar>
                    </IconButton>
                </Toolbar>
            </AppBar>
            <Drawer
                open={drawerOpen}
                variant={'permanent'}
                anchor="left"
            >
                <List style={{marginTop: '50px',
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                    justifyContent: 'center',
                    height: '100%', width:'100%'}}>
                    {sideNavBarSections.map((btn, index) => (
                        <DrawerListItem key={index} onClick={() => handleSectionChange(index)} isactive={(activeSectionIndex === index).toString()} itemscount={sideNavBarSections.length}>
                            <DrawerButton
                                disabled={activeSectionIndex === index}
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
                                {drawerOpen && (<ListItemText primary={btn.title}/>)}
                            </DrawerButton>
                        </DrawerListItem>
                    ))}
                </List>
            </Drawer>
            {children}
        </div>
    );
};

export default AppOverlay;
