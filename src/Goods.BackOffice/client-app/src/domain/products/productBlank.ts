import { Product } from './product';
import { ProductCategory } from './productCategory';

export class ProductBlank {
	constructor(
		public id: string | null,
		public category: ProductCategory | null,
		public name: string | null,
		public description: string | null,
		public price: number | null
	) { }
}

export namespace ProductBlank {
	export function getEmpty(): ProductBlank {
		return new ProductBlank(null, null, null, null, null);
	}

	export function getFromProduct(product: Product): ProductBlank {
		return {
			id: product.id,
			category: product.category,
			name: product.name,
			description: product.description,
			price: product.price
		};
	}
}
