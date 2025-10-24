import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {ButtonComponent, DataInputComponent, ValidationMessageComponent} from "shared/components";
import {FormBuilder, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {StepComponentBase} from "../step-component-base";
import {enterTrigger} from "../../auth.animations";
import {AppCrossValidators, AppValidators} from "shared/validators";

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

    public override readonly form = this._formBuilder.group({
        password: this._formBuilder.nonNullable.control("", [
            AppValidators.required("Password"), AppValidators.password
        ]),
        passwordConfirm: this._formBuilder.nonNullable.control("")
    }, {
        validators: [AppCrossValidators.equal("password", "passwordConfirm")]
    });

    public constructor() {
        super("signup/completed");
    }
}
