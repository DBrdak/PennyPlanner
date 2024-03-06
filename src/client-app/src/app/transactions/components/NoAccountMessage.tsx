import {Button, Stack, Typography} from "@mui/material";
import {ArrowForward} from "@mui/icons-material";
import {useNavigate} from "react-router-dom";

export function NoAccountMessage() {
    const navigate = useNavigate()

    return (
        <Stack dir={'column'} spacing={4} sx={{
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center'
        }}>
            <Typography variant={'h3'} sx={{
                userSelect: 'none',
                textAlign: 'center'
            }}>
                Please add account first
            </Typography>
            <Button color={'primary'} variant={'contained'} onClick={() => navigate('/accounts/new')} sx={{
                width: '50%'
            }}>
                <ArrowForward fontSize={'large'} />
            </Button>
        </Stack>
    );
}