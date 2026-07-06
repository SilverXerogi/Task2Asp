import React from 'react';
import { Route } from 'react-router-dom';
import { ProductsPage } from './productsPage';

export function ProductsRouter() {
	return (
		<>
			<Route path={ProductLink.index} element={<ProductsPage />} />
		</>
	);
}

export class ProductLink {
	public static index = '/products';
}
