import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {ButtonComponent, DataInputComponent, ValidationMessageComponent} from "shared/components";
import {FormBuilder, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {StepComponentBase} from "../step-component-base";
import {enterTrigger} from "shared/animations";
import {AppValidators, CoercionDirective} from "shared/validators";

@Component({
    selector: 'auth-second-step',
    standalone: true,
    imports: [
        ButtonComponent,
        DataInputComponent,
        FormsModule,
        ReactiveFormsModule,
        ValidationMessageComponent,
        CoercionDirective
    ],
    templateUrl: './second-step.component.html',
    styleUrl: './second-step.component.scss',
    animations: [
        enterTrigger
    ],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class SecondStepComponent extends StepComponentBase{
    private readonly _formBuilder = inject(FormBuilder);

    public override readonly form = this._formBuilder.group({
        firstName: this._formBuilder.nonNullable.control("",
            [AppValidators.required("First name")]),
        lastName: this._formBuilder.nonNullable.control("",
            [AppValidators.required("Second name")]),
        middleName: this._formBuilder.nonNullable.control("")
    });

    constructor() {
        super("signup/step3");
    }
}
