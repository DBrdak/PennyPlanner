import React from 'react';
import { SpeedDial, SpeedDialAction, SpeedDialIcon } from '@mui/material';
import {
    Add as AddIcon,
    SyncAlt as SyncAltIcon,
    ReceiptTwoTone, South, North
} from '@mui/icons-material';
import theme from "../../app/theme";
import {useNavigate} from "react-router-dom";

const TransactionButtons: React.FC = () => {
    const navigate = useNavigate()
    const [open, setOpen] = React.useState(false);
    const actions = [
        { icon: <South color={'success'} />, name: 'Income', path: '/transactions/income' },
        { icon: <North color={'error'} />, name: 'Outcome', path: '/transactions/outcome' },
        { icon: <SyncAltIcon color={'info'} />, name: 'Internal Transaction', path: '/transactions/internal' },
    ];

    const handleOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const handleAction = (path: string) => {
        setOpen(false);
        navigate(path);
    };

    return (
        <>
            <SpeedDial
                ariaLabel="Transaction SpeedDial"
                icon={<SpeedDialIcon icon={<ReceiptTwoTone />} openIcon={<AddIcon />} />}
                open={open}
                direction="up"
                onMouseEnter={handleOpen}
                onMouseLeave={handleClose}
                sx={{
                    position: 'fixed',
                    bottom: theme.spacing(2),
                    right: theme.spacing(2),
                }}
            >
                {actions.map((action) => (
                    <SpeedDialAction
                        key={action.name}
                        icon={action.icon}
                        tooltipTitle={action.name}
                        onClick={() => handleAction(action.path)}
                    />
                ))}
            </SpeedDial>
        </>
    );
};

export default TransactionButtons;
