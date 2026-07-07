import { StudentGroup } from './StudentGroup';
import { StudyFormat } from './StudyFormat';

export class StudentGroupBlank {
	constructor(
		public id: string | null,
		public name: string | null,
		public abbr: string | null,
		public startYear: number | null,
		public endYear: number | null,
		public studyFormat: StudyFormat | null
	) { }
}

export namespace StudentGroupBlank {
	export function getEmpty(): StudentGroupBlank {
		return new StudentGroupBlank(null, null, null, null, null, null);
	}

	export function getFromStudentGroup(studentGroup: StudentGroup): StudentGroupBlank {
		return {
			id: studentGroup.id,	
			name: studentGroup.name,
			abbr: studentGroup.abbr,
			startYear: studentGroup.startYear,
			endYear: studentGroup.endYear,
			studyFormat: studentGroup.studyFormat,
		};
	}
}
