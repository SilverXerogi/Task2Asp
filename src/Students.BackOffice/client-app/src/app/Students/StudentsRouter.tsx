import React from 'react';
import { Route } from 'react-router-dom';
import { StudentsPage } from './StudentsPage';

export function StudentsRouter() {
	return (
		<>
			<Route path={StudentLink.index} element={<StudentsPage />} />
		</>
	);
}

export class StudentLink {
	public static index = '/students';
}
