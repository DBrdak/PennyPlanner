import {styled} from "@mui/material";
import MuiPaper from "@mui/material/Paper";
import {useEffect, useState} from "react";

interface TilePaperProps {
    disableBorder?: boolean
    disabled?: boolean
    invert?: boolean
}

const TilePaper = styled(MuiPaper, {shouldForwardProp: propName =>
            propName !== 'invert' && propName !== 'disableBorder' && propName !== 'disabled'})
    <TilePaperProps>(
    ({ theme, disableBorder, disabled, invert }) => {
        const [isMounted, setIsMounted] = useState(false)

        useEffect(() => {
            const timeoutId = setTimeout(() => {
                setIsMounted(true);
            }, 100);

            return () => clearTimeout(timeoutId);
        }, [])

        return {
            minHeight: '12rem',
            position: "relative",
            overflow: "hidden",
            background: `linear-gradient(to bottom, ${theme.palette.background.default}, ${theme.palette.background.paper})`,
            display: "flex",
            justifyContent: "center",
            alignItems: "start",
            paddingTop: "3%",
            height: "100%",
            transform: isMounted ? "scale(1)" : "scale(0)",
            transition: "transform 0.5s ease",
            ...(!disableBorder && {
                border: 'thin solid transparent',
                borderImage: `linear-gradient(to bottom left, ${theme.palette.primary.main}, ${theme.palette.secondary.main})`,
                borderImageSlice: 1,
            }),
            ...(invert && {
                background: `linear-gradient(to bottom left, ${theme.palette.primary.dark}, ${theme.palette.secondary.main})`,
                border: 'none'
            }),
            ...(!disabled && {
                cursor: "pointer",
                '&:hover': {
                    transform: isMounted ? 'translate(-1.5%, -1.5%)' : 'scale(0)',
                    boxShadow: '8px 8px 15px rgba(0, 0, 0, 0.6)',
                },
                '&:active': {
                    transform: 'scale(0.9)',
                    boxShadow: '0 0 100px rgba(0, 0, 0, 0.6) inset',
                },
            }),
        }

    }
)


export default TilePaper