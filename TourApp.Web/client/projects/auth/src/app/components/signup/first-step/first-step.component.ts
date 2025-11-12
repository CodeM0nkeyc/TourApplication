import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {ButtonComponent, DataInputComponent, ValidationMessageComponent} from "shared/components";
import {FormBuilder, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {StepComponentBase} from "../step-component-base";
import {enterTrigger} from "../../auth.animations";
import {AppValidators, CoercionDirective} from "shared/validators";
import {UsersService} from "shared/services";

@Component({
    selector: 'auth-first-step',
    standalone: true,
    imports: [
        ButtonComponent,
        DataInputComponent,
        FormsModule,
        ReactiveFormsModule,
        CoercionDirective,
        ValidationMessageComponent
    ],
    templateUrl: './first-step.component.html',
    styleUrl: './first-step.component.scss',
    animations: [
        enterTrigger
    ],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class FirstStepComponent extends StepComponentBase {
    private readonly _formBuilder = inject(FormBuilder);
    private readonly _usersHttpService = inject(UsersService);

    public override readonly form = this._formBuilder.group({
        email: this._formBuilder.nonNullable.control("", [
            AppValidators.required("Email"), AppValidators.email
        ]),
        phoneNumber: this._formBuilder.nonNullable.control("", [
            AppValidators.phoneNumber(true)
        ])
    });

    public constructor() {
        super("signup/step2");
    }

    public override async submit(): Promise<boolean> {
        const userExistsRequestResult = await this._usersHttpService.userExistsAsync(
            this.form.value.email!);

        const userExists = userExistsRequestResult.data;

        if (!userExists) {
            return super.submit();
        }

        this.form.controls.email.setErrors({ emailExists: "This email is already in use" });

        return false;
    }
}
