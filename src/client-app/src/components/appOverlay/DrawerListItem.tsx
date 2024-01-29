import { styled, css } from "@mui/system";
import MuiListItem from "@mui/material/ListItem";
import theme from "../../app/theme";

interface CustomListItemOwnProps {
    isactive: string;
    itemscount: number;
}

const DrawerListItem = styled(MuiListItem)<CustomListItemOwnProps>(
    ({ isactive, itemscount }) => css`
    background-color: ${isactive === 'true'
        ? theme.palette.background.default
        : theme.palette.background.paper};
    border-radius: 0;
    padding: 0;
    width: 100%;
    text-align: center;
    height: ${100 / itemscount}%;
    cursor: pointer;
  `
);

export default DrawerListItem;
