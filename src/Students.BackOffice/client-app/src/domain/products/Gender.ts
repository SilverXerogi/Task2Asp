export enum ProductCategory {
	Male = 1,
	Female = 2,
	
}

export namespace ProductCategory {
	export const getDisplayName = (category: ProductCategory): string => {
		switch (category) {
			case ProductCategory.Male:
				return 'Мужчина';
			case ProductCategory.Female:
				return 'Женщина';
			
		}
	};
}
