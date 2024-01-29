import {styled} from "@mui/material";
import MuiPaper from "@mui/material/Paper";

interface TilePaperProps {
    img: string
}

const TilePaper = styled(MuiPaper)<TilePaperProps>(
    ({ theme, img }) => ({
    position: 'relative',
    overflow: 'hidden',
    backgroundImage: `url(${img})`,
    backgroundSize: 'cover',
    backgroundPosition: 'center',
    boxShadow: '0 0 100px rgba(0, 0, 0, 1) inset',
    display: 'flex',
    borderRadius: '20px',
    justifyContent: 'center',
    alignItems: 'center',
    height: '100%',
    cursor: 'pointer',
    transition: 'transform 0.5s',
    '&:hover': {
        transform: 'translate(-1.5%, -1.5%)',
        boxShadow: '8px 8px 15px rgba(0, 0, 0, 0.6)',
    },
    '&:active': {
        transform: 'scale(0.9)',
        boxShadow: '0 0 100px rgba(0, 0, 0, 0.6) inset',
    }
}))


export default TilePaper