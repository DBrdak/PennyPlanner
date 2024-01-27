import {CSSObject, IconButtonProps, styled, Theme} from "@mui/material";
import MuiIconButton from '@mui/material/IconButton';
import theme from "../../app/theme";
import {drawerWidth} from "./Drawer";

interface CustomIconButtonProps extends IconButtonProps {
    isOpen: boolean;
}

const openedMixin = (theme: Theme): CSSObject => ({
    width: drawerWidth,
    transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.enteringScreen,
    }),
});

const closedMixin = (theme: Theme): CSSObject => ({
    transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.leavingScreen,
    }),
    width: `calc(${theme.spacing(7)} + 1px)`,
    [theme.breakpoints.up('sm')]: {
        width: `calc(${theme.spacing(8)} + 1px)`,
    },
});

const DrawerNavButton = styled(MuiIconButton, {
    shouldForwardProp: (prop) => prop !== 'isOpen',
})<CustomIconButtonProps>(({ isOpen }) => ({
    width: drawerWidth,
    flexShrink: 0,
    whiteSpace: 'nowrap',
    boxSizing: 'border-box',
    borderRadius: 0,
    height: '100%',
    aspectRatio: 1,
    '&:hover': {
        backgroundColor: "#2C2C2C"
    },
    ...(isOpen && {
        ...openedMixin(theme),
        '& .MuiDrawer-paper': openedMixin(theme),
    }),
    ...(!isOpen && {
        ...closedMixin(theme),
        '& .MuiDrawer-paper': closedMixin(theme),
    })
}))

export default DrawerNavButton