export enum StudyFormat {
	FullTime = 1,
	PartTime = 2,
	Evening = 3
}

export namespace StudyFormat {
	export const getDisplayName = (category: StudyFormat): string => {
		switch (category) {
			case StudyFormat.FullTime:
				return 'Очная';
			case StudyFormat.PartTime:
				return 'Заочная';
			case StudyFormat.Evening:
				return 'Вечерняя';
			
		}
	};
}
