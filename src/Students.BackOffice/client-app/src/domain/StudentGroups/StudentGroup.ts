import { Page } from '../../tools/types/page';
import { StudyFormat } from './StudyFormat';

export class StudentGroup {
	constructor(
		public readonly id: string,
		public readonly name: string,
		public readonly abbr: string,
		public readonly startYear: number,
		public readonly endYear: number,
		public readonly studyFormat: StudyFormat,
		public readonly period: string
	) { }
}

export function mapToStudentGroupsPage(data: any): Page<StudentGroup> {
	return Page.convert(data, mapToStudentGroup);
}

export function mapToStudentGroups(data: any[]): StudentGroup[] {
	return data.map(mapToStudentGroup);
}

export function mapToStudentGroup(data: any): StudentGroup {
	return new StudentGroup(
		data.id,
		data.name,
		data.abbr,
		data.startYear,
		data.endYear,
		data.studyFormat,
		`${data.startYear} - ${data.endYear}`
	);
}
