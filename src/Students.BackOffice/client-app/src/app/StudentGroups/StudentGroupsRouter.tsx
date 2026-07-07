import React from 'react';
import { Route } from 'react-router-dom';
import { StudentGroupsPage } from './StudentGroupsPage';

export function StudentGroupsRouter() {
	return (
		<>
			<Route path={StudentGroupLink.index} element={<StudentGroupsPage />} />
		</>
	);
}

export class StudentGroupLink {
	public static index = '/studentGroups';
}
