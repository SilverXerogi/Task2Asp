import { TextField } from '@mui/material';
import React, { ChangeEvent } from 'react';
import { InputProps as DefaultProps } from '../input';

export interface Props extends DefaultProps {
	placeholder?: string;

	value: string | null;
	onChange: (value: string | null) => void;

	isPassword?: boolean;
	required?: boolean;
}

export function TextInput(props: Props) {
	function onChange(event: ChangeEvent<HTMLInputElement>) {
		let value: string | null = event.target.value;
		if (String.isNullOrWhitespace(value)) value = null;

		props.onChange(value);
	}

	return (
		<TextField
			type={props.isPassword ? 'password' : 'text'}
			label={props.title}
			autoComplete={'new-password'}
			placeholder={props.placeholder}
			className={props.className}
			size={props.size}
			sx={props.sx}
			value={props.value ?? ''}
			onChange={onChange}
			required={props.required}
			disabled={props.disabled}
			fullWidth
		/>
	);
}
