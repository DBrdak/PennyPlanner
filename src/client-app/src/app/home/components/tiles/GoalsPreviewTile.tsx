import TilePaper from "../../../../components/tilesLayout/TilePaper";
import {useNavigate} from "react-router-dom";
import theme from "../../../theme";
import {EmojiEventsTwoTone} from "@mui/icons-material";
import {Typography} from "@mui/material";

export function GoalsPreviewTile() {
    const navigate = useNavigate()

    //TODO Display crucial info about goals and on click navigate to goals
    return (
        <TilePaper disabled onClick={() => navigate('/goals')} sx={{
            alignItems: 'center',
            flexDirection: 'column',
            gap: theme.spacing(2),
            userSelect: 'none'
        }}>
            <EmojiEventsTwoTone color={'primary'} sx={{fontSize: theme.spacing(10)}} />
            <Typography variant={'h4'}>
                Goals
            </Typography>
            <Typography variant={'h6'} color={'palegoldenrod'}>
                Available soon!
            </Typography>
        </TilePaper>
    );
}