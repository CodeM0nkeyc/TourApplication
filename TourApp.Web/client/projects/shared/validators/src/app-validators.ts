import {AbstractControl, ValidationErrors, ValidatorFn, Validators} from "@angular/forms";
import {validationRegexes} from "./common/validation-regexes";

export class AppValidators {
    public static required(fieldName?: string, isEmpty?: (value: any) => boolean): ValidatorFn {
        return (control: AbstractControl): ValidationErrors | null => {
            let isValid: boolean = control.value;

            if (isEmpty) {
                isValid = !isEmpty(control.value);
            }

            return !isValid
                ? { required: `${fieldName ?? "This"} field is required` }
                : null;
        }
    }

    public static email(control: AbstractControl) : ValidationErrors | null {
        const isValid = Validators.email(control);

        return isValid
            ? { email: "Email is incorrect" }
            : null;
    }

    public static phoneNumber(optional: boolean = false): ValidatorFn {
        return (control: AbstractControl): ValidationErrors | null => {
            const value = control.value;

            if (optional && value === "") {
                return null;
            }

            return !validationRegexes.phoneNumberWithCoercion.test(value)
                ? { phoneNumber: "Phone number is invalid" }
                : null;
        }
    }

    public static password(control: AbstractControl) : ValidationErrors | null {
        const value = control.value;

        return !validationRegexes.password.test(value)
            ? { password: "Password must have at least 8 characters, where one uppercase, lowercase, " +
                    "special symbol (@$!%*?&_) and number must be included" }
            : null;
    }
}
