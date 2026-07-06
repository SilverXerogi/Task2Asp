import { AppBar, Box, Typography } from '@mui/material';
import React from 'react';
import { Outlet } from 'react-router-dom';

export function Layout() {
	return (
		<>
			<AppBar position='fixed' sx={{ height: 64 }}>
				<Box
					sx={{
						display: 'flex',
						width: '100%',
						height: '100%',
						alignItems: 'center',
						gap: 4,
						paddingX: 2
					}}>
					<Box sx={{ width: 'fit-content', height: '100%', display: 'flex', alignItems: 'center' }}>
						<Typography sx={{ fontWeight: 'bold' }}>Goods</Typography>
					</Box>
				</Box>
			</AppBar>
			<Box
				sx={{
					width: '100%',
					height: '100%',
					paddingTop: 10,
					paddingBottom: 2,
					paddingX: 2
				}}>
				<Outlet />
			</Box>
		</>
	);
}
