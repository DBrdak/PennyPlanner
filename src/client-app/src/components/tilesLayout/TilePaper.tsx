import {styled, useMediaQuery} from "@mui/material";
import MuiPaper from "@mui/material/Paper";
import theme from "../../app/theme";
import {useEffect, useState} from "react";

interface TilePaperProps {
    colors?: string | null
}

const randomGradient = (colors?: string | null) => {
    const randomNumber = Math.floor(Math.random() * 100)

    if(colors === 'cyan'){
        return `linear-gradient(to bottom right, ${theme.palette.secondary.dark}, ${theme.palette.secondary.dark})`;
    }
    if(colors === 'magenta'){
        return `linear-gradient(to bottom right, ${theme.palette.primary.dark}, ${theme.palette.primary.dark})`;
    }
    if (randomNumber % 19 === 0) {
        return `linear-gradient(to bottom right, ${theme.palette.primary.dark}, ${theme.palette.secondary.light})`;
    } else if (randomNumber % 17 === 0) {
        return `linear-gradient(to bottom left, ${theme.palette.primary.light}, ${theme.palette.secondary.dark})`;
    } else if (randomNumber % 13 === 0) {
        return `linear-gradient(to top right, ${theme.palette.primary.main}, ${theme.palette.secondary.light})`;
    } else if (randomNumber % 11 === 0) {
        return `linear-gradient(to left, ${theme.palette.primary.dark}, ${theme.palette.secondary.main})`;
    } else if (randomNumber % 7 === 0) {
        return `linear-gradient(to right, ${theme.palette.primary.light}, ${theme.palette.secondary.dark})`;
    } else if (randomNumber % 6 === 0) {
        return `linear-gradient(to bottom left, ${theme.palette.primary.main}, ${theme.palette.secondary.light})`;
    } else if (randomNumber % 5 === 0) {
        return `linear-gradient(to top right, ${theme.palette.primary.dark}, ${theme.palette.secondary.dark})`;
    } else if (randomNumber % 3 === 0) {
        return `linear-gradient(to bottom right, ${theme.palette.primary.light}, ${theme.palette.secondary.main})`;
    } else if (randomNumber % 2 === 0) {
        return `linear-gradient(to top left, ${theme.palette.primary.main}, ${theme.palette.secondary.main})`;
    } else {
        return `linear-gradient(to top, ${theme.palette.primary.light}, ${theme.palette.secondary.dark})`;
    }
}

const TilePaper = styled(MuiPaper)<TilePaperProps>(
    ({ theme, colors }) => {
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
            background: randomGradient(colors),
            display: "flex",
            borderRadius: "20px",
            justifyContent: "center",
            alignItems: "start",
            paddingTop: "3%",
            height: "100%",
            cursor: "pointer",
            transform: isMounted ? "scale(1)" : "scale(0)",
            transition: "transform 0.5s ease",
            "&:hover": {
                transform: isMounted ? "translate(-1.5%, -1.5%)" : "scale(0)",
                boxShadow: "8px 8px 15px rgba(0, 0, 0, 0.6)",
            },
            "&:active": {
                transform: "scale(0.9)",
                boxShadow: "0 0 100px rgba(0, 0, 0, 0.6) inset",
            },
        }
    }
)


export default TilePaper