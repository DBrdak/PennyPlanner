import {styled} from "@mui/system";
import MuiBox from '@mui/material/Box'
import theme from "../theme";
import '../../styles/index.css'

const GradientContainer = styled(MuiBox)({
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center',
    minHeight: '100vh',
    background: `linear-gradient(45deg, ${theme.palette.primary.light}, ${theme.palette.secondary.dark})`,
    animation: `gradientAnimation 10s infinite linear`,
});

export default GradientContainer