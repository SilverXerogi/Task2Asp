import React from 'react';
import { Route } from 'react-router-dom';
import { StudentGroupsPage } from './StudentGroupsPage';

export function ProductsRouter() {
	return (
		<>
			<Route path={ProductLink.index} element={<StudentGroupsPage />} />
		</>
	);
}

export class ProductLink {
	public static index = '/products';
}
