import {Stack, Typography} from "@mui/material";
import {ExpandLessTwoTone, ExpandMoreTwoTone} from "@mui/icons-material";

interface TransactionsTableGroupHeaderProps {
    groupKey: string;
    isCollapsed: boolean;
    isHovered: boolean;
    onClick: () => void;
    onMouseEnter: () => void;
    onMouseLeave: () => void;
}

export default function TransactionsTableGroupHeader({
        groupKey,
        isCollapsed,
        isHovered,
        onClick,
        onMouseEnter,
        onMouseLeave,
}: TransactionsTableGroupHeaderProps) {

    return (
        <Stack
            onClick={onClick}
            onMouseEnter={onMouseEnter}
            onMouseLeave={onMouseLeave}
            sx={{
                cursor: 'pointer',
                borderRadius: '10px 10px 0px 0px',
                padding: '0.75rem',
                userSelect: 'none',
                backgroundColor: isHovered ? '#333' : '#121212',
                marginTop: '4px',
                flexDirection: 'row',
                justifyContent: 'space-between',
                position: 'sticky',
                top: 0,
                zIndex: 100
            }}
        >
            {isCollapsed ? (
                isHovered ? (
                    <ExpandMoreTwoTone fontSize={'large'} />
                ) : (
                    <ExpandLessTwoTone fontSize={'large'} />
                )
            ) : isHovered ? (
                <ExpandLessTwoTone fontSize={'large'} />
            ) : (
                <ExpandMoreTwoTone fontSize={'large'} />
            )}
            <Typography variant={'h5'}>
                {groupKey}
            </Typography>
            {isCollapsed ? (
                isHovered ? (
                    <ExpandMoreTwoTone fontSize={'large'} />
                ) : (
                    <ExpandLessTwoTone fontSize={'large'} />
                )
            ) : isHovered ? (
                <ExpandLessTwoTone fontSize={'large'} />
            ) : (
                <ExpandMoreTwoTone fontSize={'large'} />
            )}
        </Stack>
    );
};