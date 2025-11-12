import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {ButtonComponent, DataInputComponent, ValidationMessageComponent} from "shared/components";
import {FormBuilder, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {StepComponentBase} from "../step-component-base";
import {enterTrigger} from "../../auth.animations";
import {AppCrossValidators, AppValidators} from "shared/validators";
import {RegistrationData, UsersService} from "shared/services";

@Component({
    selector: 'auth-fourth-step',
    standalone: true,
    imports: [
        ButtonComponent,
        DataInputComponent,
        FormsModule,
        ReactiveFormsModule,
        ValidationMessageComponent
    ],
    templateUrl: './fourth-step.component.html',
    styleUrl: './fourth-step.component.scss',
    animations: [
        enterTrigger
    ],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class FourthStepComponent extends StepComponentBase {
    private readonly _formBuilder = inject(FormBuilder);
    private readonly _usersService = inject(UsersService);

    public override readonly form = this._formBuilder.group({
        password: this._formBuilder.nonNullable.control("", [
            AppValidators.required("Password"), AppValidators.password
        ]),
        passwordConfirm: this._formBuilder.nonNullable.control("")
    }, {
        validators: [AppCrossValidators.equal("password", "passwordConfirm")]
    });

    public constructor() {
        super(`signup/confirmation`);
    }

    private getRegistrationData(): RegistrationData {
        return {
            email: super.getSavedData("email"),
            phoneNumber: super.getSavedData("phoneNumber"),
            firstName: super.getSavedData("firstName"),
            lastName: super.getSavedData("lastName"),
            middleName: super.getSavedData("middleName"),
            address: {
                country: super.getSavedData("country"),
                region: super.getSavedData("region"),
                city: super.getSavedData("city"),
            },
            password: this.form.value.password!,
        };
    }

    protected override saveData(): void {

    }

    public override async submit(): Promise<boolean> {
        const registrationData = this.getRegistrationData();

        const registrationResult = await this._usersService.registerAsync(registrationData);

        if (registrationResult.isSuccess) {
            this.nextUrl += `?email=${registrationData.email}`;
            sessionStorage.clear();
            return super.submit();
        }

        return false;
    }
}
