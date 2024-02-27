import {AllSeriesType, PieChart, PiePlot, PieValueType, ResponsiveChartContainer, useDrawingArea} from "@mui/x-charts";
import {MakeOptional} from "@mui/x-charts/models/helpers";
import theme from "../app/theme";
import {Box, Container, styled, Typography} from "@mui/material";

interface TwoFactorPieChartProps {
    actual: number
    target: number
    color: 'positive' | 'negative'
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

export function TwoFactorPieChart({ actual, target, color }: TwoFactorPieChartProps) {

    const getData = () => {
        const data: MakeOptional<PieValueType, "id">[] = []

        if(actual > 0) {
            data.push({
                id: 1,
                value: actual,
                color: color === 'positive'
                    ? theme.palette.success.light
                    : theme.palette.error.light
            })
        }

        data.push({
            id: 2,
            value: target - actual,
            color: theme.palette.text.primary
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
                paddingAngle: 2,
                startAngle: 0,
                endAngle: 360,
            }
        ]} title={'Miur'}>
            <PiePlot tooltip={{ trigger: 'none' }}>
            </PiePlot>
        </ResponsiveChartContainer>

    );
}