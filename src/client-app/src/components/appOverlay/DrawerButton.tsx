import { styled, css } from "@mui/system";
import MuiListItemButton from "@mui/material/ListItemButton";

const DrawerButton = styled(MuiListItemButton)(
    () => css`
        height: 100%;
        text-align: center;
        '& .Mui-disabled': {
          opacity: 1;
        }
  `
);

export default DrawerButton;
