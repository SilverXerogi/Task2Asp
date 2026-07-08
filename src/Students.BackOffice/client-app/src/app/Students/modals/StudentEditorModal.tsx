import React, { useEffect, useState } from 'react';
import { Student } from '../../../domain/Students/Student';
import { StudentBlank } from '../../../domain/Students/StudentBlank';
import { Gender } from '../../../domain/Students/Gender';
import { StudentsProvider } from '../../../domain/Students/StudentsProvider';
import { Button } from '../../../shared/components/buttons/button';
import { Input } from '../../../shared/components/inputs/input';
import { Modal } from '../../../shared/components/modals/modal';
import { Notification } from '../../../shared/components/notification';
import { Enum } from '../../../tools/types/enum';
import { StudentGroup } from '../../../domain/StudentGroups/StudentGroup';
//import { StudentGroupsProvider } from '../../../domain/StudentGroups/StudentGroupsProvider';

interface Props {
	studentId: string | null;
	onClose: (isEdited: boolean) => void;
	isOpen: boolean;
}

export function StudentEditorModal(props: Props) {
	const [studentBlank, setStudentBlank] = useState<StudentBlank>(StudentBlank.getEmpty());
	const [errorMessage, setErrorMessage] = useState<string | null>(null);
	const [studentGroups, setStudentGroups] = useState<StudentGroup[]>([]);

	useEffect(() => {
		if (!props.isOpen) return;
		async function loadData() {
			//const studentGroupsPage = await StudentGroupsProvider.getStudentGroupsPage(1, 15);
			//setStudentGroups(studentGroupsPage.values); загрузка списка групп для отображения в списке
			
			let studentBlank: StudentBlank | null = null;

			if (props.studentId != null) {
				const student: Student | null = await StudentsProvider.getStudentById(props.studentId);
				if (student == null) throw 'Student is null';

				studentBlank = StudentBlank.getFromStudent(student);
			}

			setStudentBlank(studentBlank ?? StudentBlank.getEmpty());
		}

		loadData();

		return () => {
			setStudentBlank(StudentBlank.getEmpty());
			setStudentGroups([]);
			setErrorMessage(null);
		};
	}, [props.isOpen, props.studentId]);

	async function saveStudent() {
		const result = await StudentsProvider.saveStudent(studentBlank);
		if (!result.isSuccess) {
			setErrorMessage(result.errorsAsString);
			return;
		}

		props.onClose(true);
	}

	function addSpecialMark() {
		setStudentBlank((studentBlank) => ({
			...studentBlank,
			specialMarks: studentBlank.specialMarks ?? []
		}));
	} 
	function removeSpecialMark(index: number) {
		setStudentBlank((studentBlank) => ({
			...studentBlank,
			specialMarks: studentBlank.specialMarks?.filter((_, i) => i != index) ?? []
		}));
	} 
	function updateSpecialMark(index: number, value: string) {
		setStudentBlank((studentBlank) => ({
			...studentBlank,
			specialMarks: studentBlank.specialMarks?.map((mark, i) => i === index ? value: mark) ?? []
		}));
	} 
	
	return (
		<>
			<Modal onClose={() => props.onClose(false)} isOpen={props.isOpen}>
				<Modal.Header onClose={() => props.onClose(false)}>Редактор студента</Modal.Header>
				<Modal.Body
					sx={{
						maxWidth: '800px',
						minWidth: '600px',
						display: 'flex',
						flexDirection: 'column',
						gap: '12px'
					}}>
					<Input
						variant='text'
						title='Введите ФИО'
						value={studentBlank.fullName}
						onChange={(fullName) => setStudentBlank((studentBlank) => ({ ...studentBlank, fullName }))}
						required
					/>
					<Input
						variant='select'
						title='Выберите пол'
						options={Enum.getNumberValues<Gender>(Gender)}
						getOptionLabel={(option) => Gender.getDisplayName(option)}
						isOptionEqualToValue={(a, b) => a === b}
						value={studentBlank.gender}
						onChange={(gender) => setStudentBlank((studentBlank) => ({ ...studentBlank, gender }))}
						required
					/>
					<Input
						variant='number'
						title='Введите возраст'
						value={studentBlank.age}
						onChange={(age) => setStudentBlank((studentBlank) => ({ ...studentBlank, age }))}
						isAvailableFractionValue
						required
					/>
					<Input
						variant='number'
						title='Введите среднюю оценку'
						value={studentBlank.averageGrade}
						onChange={(averageGrade) => setStudentBlank((studentBlank) => ({ ...studentBlank, averageGrade }))}
						isAvailableFractionValue
						required
					/>
					<Input
						variant='select'
						title='Выберите группу'
						options={studentGroups.map(g => g.id)}
						getOptionLabel={(option) => {
							const group = studentGroups.find(g => g.id === option);
							return group ? '${group.abrr} - ${group.name}' : ''
						}}
						isOptionEqualToValue={(a, b) => a === b}
						value={studentBlank.studentGroupId}
						onChange={(studentGroupId) => setStudentBlank((studentBlank) => ({ ...studentBlank, studentGroupId }))}
						required
					/>
					<div style={{ display: 'flex', flexDirection: 'column', gap: '8px' }}>
						<div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
							<span style={{fontWeight: 'bold'} }>Особые отметки</span>
							<Button variant='add' title='add' size='small' onClick={addSpecialMark} />
						</div>
						{studentBlank.specialMarks?.map((mark, index) => (
							<div key={'mark_${index}'} style={{ display: 'flex', gap: '8px' }}>
								<Input
									variant='text'
									title={'mark ${index+1}'}
									value={mark}
									onChange={(value)=>updateSpecialMark(index, value ?? '') }
								/>
								<Button
									variant='remove'
									size='small'
									onClick={()=> removeSpecialMark(index) }
								/>
							</div>
						))}
					</div>
				</Modal.Body>
				<Modal.Footer>
					<Button variant='save' onClick={() => saveStudent()} />
				</Modal.Footer>
			</Modal>
			
			{
				!String.isNullOrWhitespace(errorMessage) &&
				<Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
			}
		</>
	);
}
