import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { AppBase } from '../shared/components/appBase';
import { Layout } from '../shared/components/layout';
import { InfrastructureRouter } from './infrastructure/infrastructureRouter';
import { StudentsRouter } from './Students/StudentsRouter';
import { StudentGroupsRouter } from './StudentGroups/StudentGroupsRouter';

export function App() {
	return (
		<AppBase>
			<BrowserRouter>
				<Routes>
					<Route element={<Layout />}>
						{InfrastructureRouter()}
						{StudentsRouter()}
						{StudentGroupsRouter()}
					</Route>
				</Routes>
			</BrowserRouter>
		</AppBase>
	);
}
