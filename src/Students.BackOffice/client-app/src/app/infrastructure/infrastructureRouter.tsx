import React from 'react';
import { Route } from 'react-router-dom';
import { Home } from "./components/home";

export function InfrastructureRouter() {
	return (
		<>
			<Route path={InfrastructureLink.index} element={<Home />} />
		</>
	);
}

export class InfrastructureLink {
	public static index = '/';
}
