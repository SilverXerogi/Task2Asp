import {
	Container,
	Paper,
	Table,
	TableBody,
	TableCell,
	TableContainer,
	TableHead,
	TableRow,
	Typography
} from '@mui/material';
import React, { useEffect, useState } from 'react';
import { StudentGroup } from '../../domain/StudentGroups/StudentGroup';
import { StudyFormat } from '../../domain/StudentGroups/StudyFormat';
import { StudentGroupsProvider } from '../../domain/StudentGroups/StudentGroupsProvider';
import { Button } from '../../shared/components/buttons/button';
import { ConfirmModal } from '../../shared/components/modals/confirmModal';
import { Notification } from '../../shared/components/notification';
import { TablePagination } from '../../shared/components/tablePagination';
import { ConfirmModalState } from '../../shared/types/confirmModalState';
import { Pagination } from '../../tools/types/pagination';
import { StudentGroupEditorModal } from './modals/StudentGroupEditorModal';

type StudentGroupEditorModalState = {
	studentGroupId: string | null;
	isOpen: boolean;
};

interface RemoveStudentGroupConfirmModalState extends ConfirmModalState {
	studentGroupId: string | null;
}

export function StudentGroupsPage() {
	const [studentGroups, setStudentGroups] = useState<StudentGroup[]>([]);
	const [pagination, setPagination] = useState<Pagination>(Pagination.default);

	const [studentGroupEditorModalState, setStudentGroupEditorModalState] = useState<StudentGroupEditorModalState>({
		studentGroupId: null,
		isOpen: false
	});
	const [removeStudentGroupConfirmModalState, setRemoveStudentGroupConfirmModalState] =
		useState<RemoveStudentGroupConfirmModalState>({ studentGroupId: null, ...ConfirmModalState.getClosed() });

	const [errorMessage, setErrorMessage] = useState<string | null>(null);

	useEffect(() => {
		loadStudentGroupsPage({ ...pagination });
	}, []);

	async function loadStudentGroupsPage(newPagination: Pagination) {
		const studentGroupsPage = await StudentGroupsProvider.getStudentGroupsPage(newPagination.page, newPagination.pageSize);

		setStudentGroups(studentGroupsPage.values);
		setPagination((pagination) => ({
			...pagination,
			page: newPagination.page,
			pageSize: newPagination.pageSize,
			totalRows: studentGroupsPage.totalRows
		}));
	}

	function openStudentGroupEditorModal(studentGroupId?: string) {
		setStudentGroupEditorModalState({ studentGroupId: studentGroupId ?? null, isOpen: true });
	}

	function closeProductEditorModal(isEdited: boolean) {
		if (isEdited) loadStudentGroupsPage({ ...pagination, page: 1 });
		setStudentGroupEditorModalState({ studentGroupId: null, isOpen: false });
	}

	function openRemoveStudentGroupConfirmModal(studentGroupId: string, studentGroupName: string) {
		setRemoveStudentGroupConfirmModalState({
			studentGroupId,
			...ConfirmModalState.getOpen(`Вы действительно хотите удалить продукт "${studentGroupName}"`)
		});
	}

	async function closeRemoveStudentGroupConfirmModal(isConfirmed: boolean) {
		if (isConfirmed) {
			if (removeStudentGroupConfirmModalState.studentGroupId == null) throw 'Cannot remove studentGroup with studentGroupId = null';

			const result = await StudentGroupsProvider.removeStudentGroup(removeStudentGroupConfirmModalState.studentGroupId);
			if (!result.isSuccess) {
				setErrorMessage(result.errors.map((error) => error.message).join('. '));
				return;
			}

			loadStudentGroupsPage({ ...pagination, page: 1 });
		}

		setRemoveStudentGroupConfirmModalState({ studentGroupId: null, ...ConfirmModalState.getClosed() });
	}

	return (
		<Container
			sx={{ height: '100%', display: 'flex', flexDirection: 'column', gap: '12px' }}
			maxWidth={false}
			disableGutters>
			<Paper
				elevation={3}
				sx={{
					display: 'flex',
					alignItems: 'center',
					justifyContent: 'space-between',
					paddingX: '12px',
					paddingY: '6px'
				}}>
				<Typography variant='h6'>Студенческие группы</Typography>
				<Button variant='add' title='Создать' onClick={() => openStudentGroupEditorModal()} />
			</Paper>
			<Paper elevation={3} sx={{ height: 'calc(100% - 52px)' }}>
				<TableContainer sx={{ height: 'inherit' }}>
					<Table stickyHeader>
						<TableHead>
							<TableRow>
								<TableCell>Название</TableCell>
								<TableCell>Аббревиатура</TableCell>
								<TableCell>Период обучения</TableCell>
								<TableCell>Формат обучения</TableCell>
								<TableCell>Управление</TableCell>
							</TableRow>
						</TableHead>
						<TableBody>
							{
								studentGroups.length === 0 &&
								<TableRow>
									<TableCell colSpan={5}>Пусто</TableCell>
								</TableRow>
							}
							{
								studentGroups.map(studentGroup => (
									<TableRow key={`group__${studentGroup.id}`}>
										<TableCell width='30%'>{studentGroup.name}</TableCell>
										<TableCell width='15%'>{studentGroup.abbr}</TableCell>
										<TableCell width='20%'>{studentGroup.period}</TableCell>
										<TableCell width='20%'>
											{StudyFormat.getDisplayName(studentGroup.studyFormat)}
										</TableCell>
										<TableCell>
											<Button
												type='icon'
												variant='edit'
												size='small'
												onClick={() => openStudentGroupEditorModal(studentGroup.id)} />
											<Button
												type='icon'
												variant='remove'
												size='small'
												onClick={() => openRemoveStudentGroupConfirmModal(studentGroup.id, studentGroup.name)} />
										</TableCell>
									</TableRow>
								))
							}
						</TableBody>
					</Table>
				</TableContainer>

				<TablePagination
					countInPageOptions={Pagination.pageSizeOptions}
					page={pagination.page}
					countInPage={pagination.pageSize}
					totalRows={pagination.totalRows}
					changePage={page => loadStudentGroupsPage({ ...pagination, page })}
					changeCountInPage={pageSize => loadStudentGroupsPage({ ...pagination, pageSize })}
				/>
			</Paper>

			<StudentGroupEditorModal
				isOpen={studentGroupEditorModalState.isOpen}
				studentGroupId={studentGroupEditorModalState.studentGroupId}
				onClose={closeProductEditorModal}
			/>

			<ConfirmModal
				title={removeStudentGroupConfirmModalState.title}
				onClose={(isConfirmed) => closeRemoveStudentGroupConfirmModal(isConfirmed)}
				isOpen={removeStudentGroupConfirmModalState.isOpen}
			/>

			{
				!String.isNullOrWhitespace(errorMessage) &&
				<Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
			}
		</Container>
	);
}
