import MuiAppBar, { AppBarProps as MuiAppBarProps } from '@mui/material/AppBar';
import { styled } from "@mui/material";
import { drawerWidth } from "./Drawer";

interface AppBarProps extends MuiAppBarProps {
    open?: boolean;
    isMobile: boolean
}

const AppBar = styled(MuiAppBar, {shouldForwardProp: (prop) => prop !== 'open' && prop !== 'isMobile'})<AppBarProps>(
    ({ theme, open , isMobile}) => {
        return {
            zIndex: theme.zIndex.drawer + 1,
            height: isMobile ?
                `140px` :
                `calc(${theme.spacing(8)} + 1px)`,
            boxShadow: 'none',
            position: 'relative',
            overflow: 'hidden',
            transition: theme.transitions.create(['width', 'margin', 'opacity'], {
                easing: theme.transitions.easing.sharp,
                duration: theme.transitions.duration.leavingScreen,
            }),
            ...(open && {
                marginLeft: drawerWidth,
                width: `calc(100% - ${drawerWidth}px)`,
                transition: theme.transitions.create(['width', 'margin', 'opacity'], {
                    easing: theme.transitions.easing.sharp,
                    duration: theme.transitions.duration.enteringScreen,
                }),
            }),
        };
    }
);

export default AppBar;
