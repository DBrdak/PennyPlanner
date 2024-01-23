import React, {useState} from 'react';
import {
    Toolbar,
    List,
    ListItem,
    ListItemIcon,
    ListItemText,
    IconButton,
    Avatar,
    ListItemButton, CssBaseline, Typography, keyframes
} from '@mui/material';
import AccountCircleIcon from '@mui/icons-material/AccountCircle';
import {
    ChevronLeft,
    ChevronRight,
    HomeTwoTone,
    CalendarMonthTwoTone,
    AssessmentTwoTone,
    EmojiEventsTwoTone,
    EditNoteTwoTone,
    AccountBalanceTwoTone
} from "@mui/icons-material";
import Drawer from "./Drawer";
import AppBar from "./AppBar";
import theme from "../../app/theme";
import '../../styles/index.css'

interface AppOverlayProps {
    children: React.ReactNode;
}

const AppOverlay: React.FC<AppOverlayProps> = ({ children }) => {
    const [drawerOpen, setDrawerOpen] = React.useState(false);
    const [selectedButton, setSelectedButton] = useState<number>(0)

    const sideNavbarButtons: {title: string, icon: React.ReactNode}[] = [
        {title: 'Home', icon: <HomeTwoTone color={'secondary'} fontSize={'large'} />},
        {title: 'Budget Plans', icon: <CalendarMonthTwoTone color={'secondary'} fontSize={'large'} />},
        {title: 'Accounts', icon: <AccountBalanceTwoTone color={'secondary'} fontSize={'large'} />},
        {title: 'Goals', icon: <EmojiEventsTwoTone color={'secondary'} fontSize={'large'} />},
        {title: 'Statistics', icon: <AssessmentTwoTone color={'secondary'} fontSize={'large'} />},
        {title: 'Settings', icon: <EditNoteTwoTone color={'secondary'} fontSize={'large'} />},
    ]

    const fadeIn = keyframes`
      0% { opacity: 0; }
      100% { opacity: 1; }
    `

    const handleDrawerClick = () => {
        setDrawerOpen(!drawerOpen)
    };

    return (
        <>
            <AppBar position="fixed">
                <Toolbar style={{height: '100%', justifyContent: 'space-between', alignItems: 'center',
                    backgroundColor: theme.palette.background.paper}}>
                    <IconButton
                        color="inherit"
                        aria-label="open drawer"
                        onClick={handleDrawerClick}
                        edge="start"
                    >
                        {!drawerOpen ? <ChevronRight /> : <ChevronLeft />}
                    </IconButton>
                    <Typography variant={'h3'} style={{userSelect:'none', fontFamily: 'Fira Sans', color: '#8f8f8f', fontWeight: 'bold'}}>
                        Domestica Budget
                    </Typography>
                    <IconButton>
                        <Avatar>
                            <AccountCircleIcon />
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
                    {sideNavbarButtons.map((btn, index) => (
                        <ListItem key={index} disablePadding
                                  style={{ backgroundColor: selectedButton === index ? theme.palette.background.default : theme.palette.background.paper,
                                      height: `${100 / sideNavbarButtons.length}%`, textAlign: 'center'}}>
                            <ListItemButton
                                sx={{
                                    height: '100%',
                                    textAlign: 'center'
                                }}
                                onClick={() => setSelectedButton(index)}
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
                            </ListItemButton>
                        </ListItem>
                    ))}
                </List>
            </Drawer>
        </>
    );
};

export default AppOverlay;
