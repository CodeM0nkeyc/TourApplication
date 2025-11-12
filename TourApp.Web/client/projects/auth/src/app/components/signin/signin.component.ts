import {ChangeDetectionStrategy, Component, HostBinding, inject} from '@angular/core';
import {ButtonComponent, DataInputComponent, ValidationMessageComponent} from "shared/components";
import {RouterLink} from "@angular/router";
import {enterTrigger} from "../auth.animations";
import {FormBuilder, ReactiveFormsModule} from "@angular/forms";
import {AppValidators, fromApiErrors, getSpecificErrorsFrom} from "shared/validators";
import {AUTH_ERR_CODES, Credentials, UsersService} from "shared/services";

@Component({
    selector: 'auth-signin',
    standalone: true,
    imports: [
        DataInputComponent,
        ButtonComponent,
        RouterLink,
        ReactiveFormsModule,
        ValidationMessageComponent
    ],
    templateUrl: './signin.component.html',
    styleUrl: './signin.component.scss',
    animations: [
        enterTrigger
    ],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class SigninComponent {
    @HostBinding("@enter") public enter = true;

    private readonly _userService = inject(UsersService);
    private readonly _formBuilder = inject(FormBuilder);

    public readonly form = this._formBuilder.group({
        email: this._formBuilder.nonNullable.control("",
            [AppValidators.required("Email"), AppValidators.email]),
        password: this._formBuilder.nonNullable.control("",
            [AppValidators.required("Password")]),
    }, { updateOn: "change" });

    public async onSubmit(event: Event): Promise<void> {
        if (this.form.valid) {
            const credentials: Credentials = {
                email: this.form.value.email!,
                password: this.form.value.password!
            };

            const logInResult = await this._userService.logInAsync(credentials);

            if (logInResult.isSuccess) {
                location.href = "/home";
                return;
            }

            const errorObject = fromApiErrors(logInResult.errors)!;

            const emailError = getSpecificErrorsFrom(errorObject, AUTH_ERR_CODES.email, AUTH_ERR_CODES.notConfirmed);
            const passwordError = getSpecificErrorsFrom(errorObject, AUTH_ERR_CODES.password);

            this.form.controls.email.setErrors(emailError);
            this.form.controls.password.setErrors(passwordError);
        }
    }
}
