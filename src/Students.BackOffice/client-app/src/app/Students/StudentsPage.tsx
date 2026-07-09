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
import { Student } from '../../domain/Students/Student';
import { Gender } from '../../domain/Students/Gender';
import { StudentsProvider } from '../../domain/Students/StudentsProvider';
import { Button } from '../../shared/components/buttons/button';
import { ConfirmModal } from '../../shared/components/modals/confirmModal';
import { Notification } from '../../shared/components/notification';
import { TablePagination } from '../../shared/components/tablePagination';
import { ConfirmModalState } from '../../shared/types/confirmModalState';
import { Pagination } from '../../tools/types/pagination';
import { StudentEditorModal } from './modals/StudentEditorModal';
import { StudentGroup } from '../../domain/StudentGroups/StudentGroup';
import { StudentGroupsProvider } from '../../domain/StudentGroups/StudentGroupsProvider';

type StudentEditorModalState = {
	studentId: string | null;
	isOpen: boolean;
};

interface RemoveStudentConfirmModalState extends ConfirmModalState {
	studentId: string | null;
}

export function StudentsPage() {
	const [students, setStudents] = useState<Student[]>([]);
	const [pagination, setPagination] = useState<Pagination>(Pagination.default);
	const [groups, setGroups] = useState<StudentGroup[]>([]);
	const [studentEditorModalState, setStudentEditorModalState] = useState<StudentEditorModalState>({
		studentId: null,
		isOpen: false
	});
	const [removeStudentConfirmModalState, setRemoveStudentConfirmModalState] =
		useState<RemoveStudentConfirmModalState>({ studentId: null, ...ConfirmModalState.getClosed() });

	const [errorMessage, setErrorMessage] = useState<string | null>(null);

	useEffect(() => {
		loadStudentsPage({ ...pagination });
		loadGroups();
	}, []);
	async function loadGroups() {
		const studentGroupsPage = await StudentGroupsProvider.getStudentGroupsPage(1, 15);
		setGroups(studentGroupsPage.values); 
	}
	async function loadStudentsPage(newPagination: Pagination) {
		const studentsPage = await StudentsProvider.getStudentsPage(newPagination.page, newPagination.pageSize);

		setStudents(studentsPage.values);
		setPagination((pagination) => ({
			...pagination,
			page: newPagination.page,
			pageSize: newPagination.pageSize,
			totalRows: studentsPage.totalRows
		}));
	}

	function openStudentEditorModal(studentId?: string) {
		setStudentEditorModalState({ studentId: studentId ?? null, isOpen: true });
	}

	function closeStudentEditorModal(isEdited: boolean) {
		if (isEdited) loadStudentsPage({ ...pagination, page: 1 });
		setStudentEditorModalState({ studentId: null, isOpen: false });
	}

	function openRemoveStudentConfirmModal(studentId: string, studentName: string) {
		console.log("remove student id", { studentId })
		setRemoveStudentConfirmModalState({
			studentId,
			...ConfirmModalState.getOpen(`Вы действительно хотите удалить продукт "${studentName}"`)
		});
	}

	async function closeRemoveStudentConfirmModal(isConfirmed: boolean) {
		if (isConfirmed) {
			if (removeStudentConfirmModalState.studentId == null) throw 'Cannot remove student with StudentId = null';

			const result = await StudentsProvider.removeStudent(removeStudentConfirmModalState.studentId);
			if (!result.isSuccess) {
				setErrorMessage(result.errors.map((error) => error.message).join('. '));
				return;
			}

			loadStudentsPage({ ...pagination, page: 1 });
		}

		setRemoveStudentConfirmModalState({ studentId: null, ...ConfirmModalState.getClosed() });
	}
	function getGroupName(groupId: string): string {
		const group = groups.find(g => g.id === groupId);
		return group ? `${group.abbr} - ${group.name}` : '-';
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
				<Typography variant='h6'>Студенты</Typography>
				<Button variant='add' title='Создать' onClick={() => openStudentEditorModal()} />
			</Paper>
			<Paper elevation={3} sx={{ height: 'calc(100% - 52px)' }}>
				<TableContainer sx={{ height: 'inherit' }}>
					<Table stickyHeader>
						<TableHead>
							<TableRow>
								<TableCell>ФИО</TableCell>
								<TableCell>Пол</TableCell>
								<TableCell>Возраст</TableCell>
								<TableCell>Средняя оценка</TableCell>
								<TableCell>Стипендия</TableCell>
								<TableCell>Особые отметки</TableCell>
								<TableCell>Группа</TableCell>
								<TableCell>Управление</TableCell>
							</TableRow>
						</TableHead>
						<TableBody>
							{
								students.length === 0 &&
								<TableRow>
									<TableCell colSpan={5}>Пусто</TableCell>
								</TableRow>
							}
							{
								students.map(student => (
									<TableRow key={`student__${student.id}`}>
										<TableCell width='25%'>{student.fullName}</TableCell>
										<TableCell width='10%'>
											{Gender.getDisplayName(student.gender)}
										</TableCell>
										<TableCell width='8%'>{student.age}</TableCell>
										<TableCell width='10%'>{student.averageGrade.toFixed(2)}</TableCell>
										<TableCell width='10%'>
											{student.hasScholarShip ? 'Да' : 'Нет'}
										</TableCell>
										<TableCell width='22%'>
											{student.specialMarksText || '—'}
										</TableCell>
										<TableCell width='22%'>
											{getGroupName(student.studentGroupId)}
										</TableCell>
										<TableCell>
											<Button
												type='icon'
												variant='edit'
												size='small'
												onClick={() => openStudentEditorModal(student.id)} />
											<Button
												type='icon'
												variant='remove'
												size='small'
												onClick={() => openRemoveStudentConfirmModal(student.id, student.fullName)} />
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
					changePage={page => loadStudentsPage({ ...pagination, page })}
					changeCountInPage={pageSize => loadStudentsPage({ ...pagination, pageSize })}
				/>
			</Paper>

			<StudentEditorModal
				isOpen={studentEditorModalState.isOpen}
				studentId={studentEditorModalState.studentId}
				onClose={closeStudentEditorModal}
			/>

			<ConfirmModal
				title={removeStudentConfirmModalState.title}
				onClose={(isConfirmed) => closeRemoveStudentConfirmModal(isConfirmed)}
				isOpen={removeStudentConfirmModalState.isOpen}
			/>

			{
				!String.isNullOrWhitespace(errorMessage) &&
				<Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
			}
		</Container>
	);
}
