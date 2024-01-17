import Image from 'next/image'
import {Button, Paper, Stack, Typography} from "@mui/material";
import Link from "next/link";

export default function Home() {
  return (
      <Stack style={{width: '100vw', height: '100vh', justifyContent: 'center', alignItems: 'center'}}>
        <Paper style={{padding: '20px', textAlign: 'center'}}>
            <Typography variant={'h3'}>Welcome to my website</Typography>
        </Paper>
      </Stack>
  )
}
