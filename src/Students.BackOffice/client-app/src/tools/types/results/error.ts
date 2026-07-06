export class Error {
	constructor(
		public key: string | null,
		public message: string
	) { }
}

export function mapToError(any: any): Error {
	const key = any.key ?? null;
	const message = any.message;

	return new Error(key, message);
}

export function mapToErrors(any: any[]): Error[] {
	return any.map(mapToError);
}
