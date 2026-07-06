export class Pagination {
	constructor(
		public readonly page: number,
		public readonly pageSize: number,
		public readonly totalRows: number
	) { }

	static get default(): Pagination {
		return new Pagination(1, Pagination.pageSize.small, 0);
	}
}

export namespace Pagination {
	export const pageSize = {
		small: 15,
		medium: 50,
		large: 100
	};

	export const pageSizeOptions = [pageSize.small, pageSize.medium, pageSize.large];
}
