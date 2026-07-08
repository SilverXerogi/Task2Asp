import { Container, Paper, Typography } from '@mui/material';
import React from 'react';
import { Button } from '../../../shared/components/buttons/button';
import { StudentLink } from '../../Students/StudentsRouter';
import { StudentGroupLink } from '../../StudentGroups/StudentGroupsRouter';

export function Home() {
	return (
		<Container
			sx={{ height: '100%', display: 'flex', flexDirection: 'column', gap: '12px' }}
			maxWidth={false}
			disableGutters>
			<Paper
				elevation={3}
				sx={{
					display: 'flex',
					flexDirection: 'column',
					alignItems: 'center',
					justifyContent: 'center',
					padding: '24px',
					gap: '16px'
				}}>
				<Typography variant='h4'>Справочник университета</Typography>
				<Typography variant='body1'>Выберите раздел:</Typography>
				<div style={{ display: 'flex', gap: '12px' }}>
					<Button
						variant='add'
						title='Студенты'
						onClick={() => window.location.href = StudentLink.index}
					/>
					<Button
						variant='add'
						title='Студенческие группы'
						onClick={() => window.location.href = StudentGroupLink.index}
					/>
				</div>
			</Paper>
		</Container>
	);
}