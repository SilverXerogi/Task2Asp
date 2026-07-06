import { Page } from '../../tools/types/page';
import { mapToResult, Result } from '../../tools/types/results/result';
import { mapToProduct, mapToProductsPage, Product } from './product';
import { ProductBlank } from './productBlank';

export class ProductsProvider {
	private static readonly headers: HeadersInit = [
		['X-Requested-With', 'XMLHttpRequest'],
		['Content-Type', 'application/json']
	];

	public static async saveProduct(productBlank: ProductBlank): Promise<Result> {
		const response = await fetch('/products/save', {
			method: 'POST',
			headers: this.headers,
			body: JSON.stringify(productBlank)
		});
		const json = await response.json();

		return mapToResult(json);
	}

	public static async getProductsPage(page: number, count: number): Promise<Page<Product>> {
		const response = await fetch(`/products/get_page?page=${page}&count=${count}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToProductsPage(json);
	}

	public static async getProductById(id: string): Promise<Product | null> {
		const response = await fetch(`/products/get_by_id?productId=${id}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToProduct(json);
	}

	public static async removeProduct(id: string): Promise<Result> {
		const response = await fetch(`/products/mark_product_as_removed?productId=${id}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToResult(json);
	}
}
