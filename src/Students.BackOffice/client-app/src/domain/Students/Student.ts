import { Page } from '../../tools/types/page';
import { Gender } from './Gender';

export class Student {
  
	constructor(
		public readonly id: string,
		public readonly fullName: string,
		public readonly gender: Gender,
		public readonly age: number,
		public readonly averageGrade: number,
		public readonly specialMarks: string[],
		public readonly studentGroupId: string
	) { }
	public get hasScholarShip(): boolean{
		return this.averageGrade >= 4;
	}
	public get specialMarksText(): string {
		return this.specialMarks.join(', ')
	}
}

export function mapToStudentsPage(data: any): Page<Student> {
	return Page.convert(data, mapToStudent);
}

export function mapToStudents(data: any[]): Student[] {
	return data.map(mapToStudent);
}

export function mapToStudent(data: any): Student {
	return new Student(
		data.id,
		data.fullName,
		data.gender,
		data.age,
		data.averageGrade,
		data.specialMarks,
		data.studentGroupId
	);
}
