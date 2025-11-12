import {ApiError} from "shared/services";
import {ValidationErrors} from "@angular/forms";

export function fromApiErrors(errors: ApiError[] | null): ValidationErrors | null {
    if (!errors) {
        return null;
    }

    const errorEntries = errors.map(error => [error.code, error.description]);

    return Object.fromEntries(errorEntries);
}

export function getSpecificErrorsFrom(source: ValidationErrors | null,
                                      ...errorCodes: string[]): ValidationErrors | null {
    if (!source) {
        return null;
    }

    let errorObject: ValidationErrors = {};
    let found = false;

    for (const errorCode of errorCodes) {
        if (Object.hasOwn(source, errorCode)) {
            errorObject[errorCode] = source[errorCode];
            found = true;
        }
    }

    return found ? errorObject : null;
}
