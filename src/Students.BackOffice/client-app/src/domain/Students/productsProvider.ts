import { Page } from '../../tools/types/page';
import { mapToResult, Result } from '../../tools/types/results/result';
import { mapToStudent, mapToStudentsPage, Student } from './Student';
import { StudentBlank } from './StudentBlank';

export class StudentsProvider {
	private static readonly headers: HeadersInit = [
		['X-Requested-With', 'XMLHttpRequest'],
		['Content-Type', 'application/json']
	];

	public static async saveStudent(studentBlank: StudentBlank): Promise<Result> {
		const response = await fetch('/students/save', {
			method: 'POST',
			headers: this.headers,
			body: JSON.stringify(StudentBlank)
		});
		const json = await response.json();

		return mapToResult(json);
	}

	public static async getStudentsPage(page: number, count: number): Promise<Page<Student>> {
		const response = await fetch(`/students/get-page?page=${page}&count=${count}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToStudentsPage(json);
	}

	public static async getStudentById(id: string): Promise<Student | null> {
		const response = await fetch(`/students/get-by-id?studentId=${id}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToStudent(json);
	}

	public static async removeStudent(id: string): Promise<Result> {
		const response = await fetch(`/students/remove?productId=${id}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToResult(json);
	}
}
