import React, { useEffect, useState } from 'react';
import { StudentGroup } from '../../../domain/StudentGroups/StudentGroup';
import { StudentGroupBlank } from '../../../domain/StudentGroups/StudentGroupBlank';
import { StudyFormat } from '../../../domain/StudentGroups/StudyFormat';
import { StudentGroupsProvider } from '../../../domain/StudentGroups/StudentGroupsProvider';
import { Button } from '../../../shared/components/buttons/button';
import { Input } from '../../../shared/components/inputs/input';
import { Modal } from '../../../shared/components/modals/modal';
import { Notification } from '../../../shared/components/notification';
import { Enum } from '../../../tools/types/enum';

interface Props {
	studentGroupId: string | null;
	onClose: (isEdited: boolean) => void;
	isOpen: boolean;
}

export function StudentGroupEditorModal(props: Props) {
	const [studentGroupBlank, setStudentGroupBlank] = useState<StudentGroupBlank>(StudentGroupBlank.getEmpty());
	const [errorMessage, setErrorMessage] = useState<string | null>(null);

	const currentYear = new Date().getFullYear();
	const validYears = Array.from({length: 5}, (_, i) => currentYear - 4 + i);

	useEffect(() => {
		if (!props.isOpen) return;

		async function loadStudentGroupBlank() {
			let studentGroupBlank: StudentGroupBlank | null = null;

			if (props.studentGroupId != null) {
				const studentGroup: StudentGroup | null = await StudentGroupsProvider.getStudentGroupById(props.studentGroupId);
				if (studentGroup == null) throw 'StudentGroup is null';

				studentGroupBlank = StudentGroupBlank.getFromStudentGroup(studentGroup);
			}

			setStudentGroupBlank(studentGroupBlank ?? StudentGroupBlank.getEmpty());
		}

		loadStudentGroupBlank();

		return () => {
			setStudentGroupBlank(StudentGroupBlank.getEmpty());
			setErrorMessage(null);
		};
	}, [props.isOpen, props.studentGroupId]);

	async function saveStudentGroup() {
		const result = await StudentGroupsProvider.saveStudentGroup(studentGroupBlank);
		if (!result.isSuccess) {
			setErrorMessage(result.errorsAsString);
			return;
		}

		props.onClose(true);
	}

	return (
		<>
			<Modal onClose={() => props.onClose(false)} isOpen={props.isOpen}>
				<Modal.Header onClose={() => props.onClose(false)}>Редактор Студентческой группы</Modal.Header>
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
						title='Введите название группы'
						value={studentGroupBlank.name}
						onChange={(name) => setStudentGroupBlank((studentGroupBlank) => ({ ...studentGroupBlank, name }))}
						required
					/>
					<Input
						variant='text'
						title='Введите аббревиатуру'
						value={studentGroupBlank.abbr}
						onChange={(abbr) =>
							setStudentGroupBlank((studentGroupBlank) => ({ ...studentGroupBlank, abbr }))
						}
					/>
					<Input
						variant='select'
						title='Введите год начала обучения'
						options={validYears}
						getOptionLabel={(option) => option.toString()}
						isOptionEqualToValue={(a, b) => a === b}
						value={studentGroupBlank.startYear}
						onChange={(startYear) => {
							if (startYear !== null) {
								setStudentGroupBlank((studentGroupBlank) => ({ ...studentGroupBlank, startYear, endYear: startYear + 4 }));
							}

						}}
						required
					/>
					<Input
						variant='select'
						title='Выберите формат обучения'
						options={Enum.getNumberValues<StudyFormat>(StudyFormat)}
						getOptionLabel={(option) => StudyFormat.getDisplayName(option)}
						isOptionEqualToValue={(a, b) => a === b}
						value={studentGroupBlank.studyFormat}
						onChange={(studyFormat) => setStudentGroupBlank((studentGroupBlank) => ({ ...studentGroupBlank, studyFormat }))}
						required
					/>
				</Modal.Body>
				<Modal.Footer>
					<Button variant='save' onClick={() => saveStudentGroup()} />
				</Modal.Footer>
			</Modal>

			{
				!String.isNullOrWhitespace(errorMessage) &&
				<Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
			}
		</>
	);
}
