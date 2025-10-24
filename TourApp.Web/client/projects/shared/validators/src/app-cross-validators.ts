import {AbstractControl, ValidationErrors, ValidatorFn} from "@angular/forms";

export class AppCrossValidators {
    public static range(fromControlName: string, toControlName: string): ValidatorFn {
        return (form: AbstractControl): ValidationErrors | null => {
            const from = form.get(fromControlName);
            const to = form.get(toControlName);

            return from && to && from.value > to.value
                ? { [`range-${fromControlName}-${toControlName}`]: "Min value can't be bigger than max" }
                : null;
        }
    }

    public static equal(firstControlName: string, secondControlName: string): ValidatorFn {
        return (form: AbstractControl): ValidationErrors | null => {
            const first = form.get(firstControlName);
            const second = form.get(secondControlName);

            return first && second && first.value !== second.value
                ? { [`equal-${firstControlName}-${secondControlName}`]: "Fields not equal" }
                : null;
        }
    }
}
