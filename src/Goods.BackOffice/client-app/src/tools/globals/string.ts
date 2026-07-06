export { };

declare global {
	interface StringConstructor {
		isNullOrEmpty(str: string | null | undefined): str is null | undefined;
		isNullOrWhitespace(str: string | null | undefined): str is null | undefined;
	}
}

String.isNullOrEmpty = function (str: string | null | undefined): str is null | undefined {
	return !str;
};

String.isNullOrWhitespace = function (str: string | null | undefined): str is null | undefined {
	return !str || str.trim().length == 0;
};
