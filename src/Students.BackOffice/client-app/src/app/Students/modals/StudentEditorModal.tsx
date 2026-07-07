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

interface Props {
	studentId: string | null;
	onClose: (isEdited: boolean) => void;
	isOpen: boolean;
}

export function StudentEditorModal(props: Props) {
	const [studentBlank, setStudentBlank] = useState<StudentBlank>(StudentBlank.getEmpty());
	const [errorMessage, setErrorMessage] = useState<string | null>(null);

	useEffect(() => {
		if (!props.isOpen) return;

		async function loadStudentBlank() {
			let studentBlank: StudentBlank | null = null;

			if (props.studentId != null) {
				const student: Student | null = await StudentsProvider.getStudentById(props.studentId);
				if (student == null) throw 'Student is null';

				studentBlank = StudentBlank.getFromStudent(student);
			}

			setStudentBlank(studentBlank ?? StudentBlank.getEmpty());
		}

		loadStudentBlank();

		return () => {
			setStudentBlank(StudentBlank.getEmpty());
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

	return (
		<>
			<Modal onClose={() => props.onClose(false)} isOpen={props.isOpen}>
				<Modal.Header onClose={() => props.onClose(false)}>Редактор продукта</Modal.Header>
				<Modal.Body
					sx={{
						maxWidth: '800px',
						minWidth: '600px',
						display: 'flex',
						flexDirection: 'column',
						gap: '12px'
					}}>
					<Input
						variant='select'
						title='Выберите категорию'
						options={Enum.getNumberValues<Gender>(Gender)}
						getOptionLabel={(option) => Gender.getDisplayName(option)}
						isOptionEqualToValue={(a, b) => a === b}
						value={studentBlank.gender}
						onChange={(gender) => setStudentBlank((studentBlank) => ({ ...studentBlank, gender }))}
						required
					/>
					<Input
						variant='text'
						title='Введите название'
						value={studentBlank.fullName}
						onChange={(fullName) => setStudentBlank((studentBlank) => ({ ...studentBlank, fullName }))}
						required
					/>
					{/* 
					<Input
						variant='text-area'
						title='Введите описание'
						minRows={2}
						value={studentBlank.}
						onChange={(description) =>
							setStudentBlank((studentBlank) => ({ ...studentBlank, description }))
						}
					/> */}
					<Input
						variant='number'
						title='Введите цену'
						value={studentBlank.age}
						onChange={(age) => setStudentBlank((studentBlank) => ({ ...studentBlank, age }))}
						isAvailableFractionValue
						required
					/>
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
