import {Directive, HostBinding, inject, OnInit} from "@angular/core";
import {Router} from "@angular/router";
import {FormGroup} from "@angular/forms";

@Directive()
export abstract class StepComponentBase implements OnInit {
    protected nextUrl: string;
    protected readonly router = inject(Router);

    public abstract readonly form: FormGroup | null;

    @HostBinding("@enter") public enter = true;

    protected constructor(nextUrl: string) {
        this.nextUrl = nextUrl;
    }

    public ngOnInit(): void {
        this.initWithSavedData();
    }

    protected saveData(): void {
        if (this.form) {
            for (let controlName of Object.keys(this.form.controls)) {
                sessionStorage.setItem(controlName, this.form!.controls[controlName].value);
            }
        }
    }

    protected initWithSavedData(): void {
        if (this.form) {
            for (let controlName of Object.keys(this.form.controls)) {
                const control = this.form.get(controlName);
                const storedValue = this.getSavedData(controlName);

                if (storedValue) {
                    control!.setValue(storedValue);
                }
            }
        }
    }

    protected getSavedData(key: string): any {
        return sessionStorage.getItem(key) ?? "";
    }

    protected async submit(): Promise<boolean> {
        return true;
    }

    public async onSubmit(event: Event): Promise<void> {
        if (!this.form) {
            this.router.navigateByUrl(this.nextUrl);
            return;
        }

        if (this.form.valid) {
            const isSubmitted = await this.submit();

            if (isSubmitted) {
                this.saveData();
                this.router.navigateByUrl(this.nextUrl);
            }
        }
        else {
            for (let control of Object.values(this.form.controls)) {
                if (control.validator !== null) {
                    control.updateValueAndValidity({ onlySelf: true });
                }
            }
        }
    }
}
