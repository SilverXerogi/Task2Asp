import { Student } from './Student';
import { Gender } from './Gender';

export class StudentBlank {
	constructor(
		public id: string | null,
		public fullName: string | null,
		public gender: Gender | null,
		public age: number | null,
		public averageGrade: number | null,
		public specialMarks: string[] | null,
		public studentGroupId: string | null
	) { }
}

export namespace StudentBlank {
	export function getEmpty(): StudentBlank {
		return new StudentBlank(null, null, null, null, null, null, null);
	}

	export function getFromStudent(student: Student): StudentBlank {
		return new StudentBlank(
			student.id,
			student.fullName,
			student.gender,
			student.age,
			student.averageGrade,
			student.specialMarks,
			student.studentGroupId,
		)
	}
}
