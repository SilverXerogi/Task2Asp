import { Error, mapToErrors } from "./error";

export type Result = SuccessResult | FailResult;

export namespace Result {
    export function success(): SuccessResult {
        return new SuccessResult();
    }

    export function fail(errors: Error[]): FailResult {
        return new FailResult(errors);
    }
}

export function mapToResult(any: any): Result {
    if (!any.isSuccess) {
        const errors = mapToErrors(any.errors);
        return Result.fail(errors);
    }

    return Result.success();
}



export type DataResult<T> = SuccessDataResult<T> | FailResult;

export namespace DataResult {
    export function success<T>(value: T): SuccessDataResult<T> {
        return new SuccessDataResult<T>(value);
    }

    export function fail(errors: Error[]): FailResult {
        return new FailResult(errors);
    }
}

export function mapToDataResult<T>(any: any, dataConverter?: (data: any) => T): DataResult<T> {
    if (!any.isSuccess) {
        const errors = mapToErrors(any.errors);
        return DataResult.fail(errors);
    }

    const data = dataConverter != undefined
        ? dataConverter(any.data)
        : any.data;

    return DataResult.success(data);
}

class SuccessDataResult<T> {
    public isSuccess: true = true;
    public data: T

    constructor(data: T) {
        this.data = data;
    }
}

class SuccessResult {
    public isSuccess: true = true;
    public hasWarning: false = false;
}

class FailResult {
    public isSuccess: false = false;
    public errors: Error[];

    constructor(errors: Error[]) {
        this.errors = errors;
    }

    public get errorsMessages(): string[] {
        return this.errors.map(error => error.message)
    }

    public get errorsAsString(): string {
        return this.errors.map(error => error.message).join('. ')
    }
}
