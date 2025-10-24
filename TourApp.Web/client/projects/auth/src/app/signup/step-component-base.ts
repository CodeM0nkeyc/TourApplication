import {Directive, HostBinding, inject} from "@angular/core";
import {Router} from "@angular/router";
import {FormGroup} from "@angular/forms";

@Directive()
export abstract class StepComponentBase {
    protected readonly nextUrl: string;
    protected readonly router = inject(Router);

    public abstract readonly form: FormGroup | null;

    @HostBinding("@enter") public enter = true;

    protected constructor(nextUrl: string) {
        this.nextUrl = nextUrl;
    }

    protected navigateNext() {
        return this.router.navigateByUrl(this.nextUrl);
    }

    public onSubmit(event: Event) {
        if (!this.form) {
            return;
        }

        if (this.form.valid) {
            Object.keys(this.form.controls).forEach(controlName =>
                sessionStorage.setItem(controlName, this.form!.controls[controlName].value));

            this.navigateNext();
        }
        else {
            this.form.updateValueAndValidity();

            Object.values(this.form.controls)
                .forEach(control => {
                    if (control.validator !== null) {
                        control.updateValueAndValidity({ onlySelf: true });
                    }
                });
        }
    }
}
