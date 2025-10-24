import {ChangeDetectionStrategy, Component} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {enterTrigger} from "../../auth.animations";
import {StepComponentBase} from "../step-component-base";

@Component({
    selector: 'auth-signup-completed',
    standalone: true,
    imports: [
        FormsModule,
        ReactiveFormsModule
    ],
    templateUrl: './signup-completed.component.html',
    styleUrl: './signup-completed.component.scss',
    animations: [
        enterTrigger
    ],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class SignupCompletedComponent extends StepComponentBase {
    public override readonly form = null;

    constructor() {
        super("signin");
    }
}
