import { Page } from '../../tools/types/page';
import { mapToResult, Result } from '../../tools/types/results/result';
import { mapToStudentGroup, mapToStudentGroupsPage, StudentGroup } from './StudentGroup';
import { StudentGroupBlank } from './StudentGroupBlank';

export class StudentGroupsProvider {
	private static readonly headers: HeadersInit = [
		['X-Requested-With', 'XMLHttpRequest'],
		['Content-Type', 'application/json']
	];

	public static async saveStudentGroup(StudentGroupBlank: StudentGroupBlank): Promise<Result> {
		const response = await fetch('/student-groups/save', {
			method: 'POST',
			headers: this.headers,
			body: JSON.stringify(StudentGroupBlank)
		});
		const json = await response.json();

		return mapToResult(json);
	}

	public static async getStudentGroupsPage(page: number, count: number): Promise<Page<StudentGroup>> {
		const response = await fetch(`/student-groups/get-page?page=${page}&count=${count}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToStudentGroupsPage(json);
	}

	public static async getStudentGroupById(id: string): Promise<StudentGroup | null> {
		const response = await fetch(`/student-groups/get-by-id?studentGroupId=${id}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToStudentGroup(json);
	}

	public static async removeStudentGroup(id: string): Promise<Result> {
		const response = await fetch(`/student-groups/remove?studentGroupId=${id}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToResult(json);
	}
}
