import {AllSeriesType, PieChart, PiePlot, PieValueType, ResponsiveChartContainer, useDrawingArea} from "@mui/x-charts";
import {MakeOptional} from "@mui/x-charts/models/helpers";
import theme from "../app/theme";
import {Box, Container, styled} from "@mui/material";
import formatNumber from "../utils/formatters/numberFormatter";

interface TwoFactorPieChartProps {
    actual: number
    target: number
    color: 'positive' | 'negative'
    currency?: string
}

const StyledText = styled('text')(({ theme }) => ({
    fill: theme.palette.text.primary,
    textAnchor: 'middle',
    dominantBaseline: 'central',
    fontSize: 20,
}));

function PieCenterLabel({ children }: { children: React.ReactNode }) {
    const { width, height, left, top } = useDrawingArea();
    return (
        <StyledText x={(left + width / 2)} y={ (top + height / 2)}>
            {children}
        </StyledText>
    );
}

function PieBottomLabel({ children }: { children: React.ReactNode }) {
    const { width, left, top } = useDrawingArea();
    return (
        <StyledText x={(left + width / 2)} y={5*top+30}>
            {children}
        </StyledText>
    );
}

export function TwoFactorPieChart({ actual, target, color, currency }: TwoFactorPieChartProps) {



    const getData = () => {
        const data: MakeOptional<PieValueType, "id">[] = []
        const first = target - (actual % target)

        if(actual % target > 0) {
            data.push({
                id: 1,
                value: actual % target,
                color: color === 'positive'
                    ? actual > target ? theme.palette.success.dark : theme.palette.success.light
                    : actual > target ? theme.palette.error.dark : theme.palette.error.light
            })
        }

        data.push({
            id: 2,
            value: first,
            color: actual >= target
                ? color === 'positive' ? theme.palette.success.light : theme.palette.error.light
                : theme.palette.text.primary
        })

        return data
    }
    return (
        <ResponsiveChartContainer series={[
            {
                type: 'pie',
                data: getData(),
                innerRadius: 75,
                outerRadius: 100,
                paddingAngle: 3,
                startAngle: 0,
                endAngle: 360,
            }
        ]} height={300}>
            <PiePlot tooltip={{ trigger: 'none' }} />
            <PieCenterLabel>
                {(actual / target * 100).toFixed(0)}%
            </PieCenterLabel>
            <PieBottomLabel>
                {formatNumber(actual)} / {formatNumber(target)} {currency && currency}
            </PieBottomLabel>
        </ResponsiveChartContainer>

    );
}