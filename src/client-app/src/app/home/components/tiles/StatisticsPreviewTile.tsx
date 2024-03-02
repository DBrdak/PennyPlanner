import TilePaper from "../../../../components/tilesLayout/TilePaper";
import {useNavigate} from "react-router-dom";
import theme from "../../../theme";
import {AssessmentTwoTone, PaymentTwoTone} from "@mui/icons-material";
import {Typography} from "@mui/material";

export function StatisticsPreviewTile() {
    const navigate = useNavigate()

    return (
        <TilePaper disabled onClick={() => navigate('/statistics')} sx={{
            alignItems: 'center',
            flexDirection: 'column',
            gap: theme.spacing(2),
            userSelect: 'none'
        }}>
            <AssessmentTwoTone color={'primary'} sx={{fontSize: theme.spacing(10)}} />
            <Typography variant={'h4'}>
                Statistics
            </Typography>
            <Typography variant={'h6'} color={'palegoldenrod'}>
                Available soon!
            </Typography>
        </TilePaper>
    );
}