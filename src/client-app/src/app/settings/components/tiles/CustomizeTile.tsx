import TilePaper from "../../../../components/tilesLayout/TilePaper";
import theme from "../../../theme";
import {Typography} from "@mui/material";
import {TuneTwoTone} from "@mui/icons-material";
import {useNavigate} from "react-router-dom";

export function CustomizeTile() {
    const navigate = useNavigate()

    return (
        <TilePaper disabled /*onClick={() => navigate('/settings/customize')}*/ sx={{
            alignItems: 'center',
            flexDirection: 'column',
            gap: theme.spacing(2),
            userSelect: 'none',
            textAlign: 'center'
        }}>
            <TuneTwoTone color={'primary'} sx={{fontSize: theme.spacing(10)}} />
            <Typography variant={'h4'}>
                Customize
            </Typography>
            <Typography variant={'subtitle1'} color={'palegoldenrod'}>
                Available soon!
            </Typography>
        </TilePaper>
    );
}