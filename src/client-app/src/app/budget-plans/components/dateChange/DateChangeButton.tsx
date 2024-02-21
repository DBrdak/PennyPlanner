import {styled} from "@mui/material";
import MuiIconButton from "@mui/material/IconButton";
import theme from "../../../theme";

interface DateChangeButtonProps {
    left?: boolean
    right?: boolean
}

const DateChangeButton = styled(MuiIconButton, {shouldForwardProp: propName =>
    propName !== 'left' && propName !== 'right'})<DateChangeButtonProps>
    (({theme, left, right}) => {
        return {
            '&:hover': {
                transition: 'transform 0.3s ease',
                background: 'transparent',
                ...(left && {transform: 'translateX(-10px) scale(1.05)'}),
                ...(right && {transform: 'translateX(10px) scale(1.05)'})
            },
            '&:active': {
                transform: 'scale(0.95)',
                transition: 'transform 0.3s ease',
            },
        }
    }
)

export default DateChangeButton