import {ChangeDetectionStrategy, Component, HostBinding} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {enterTrigger} from "../../auth.animations";

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
export class SignupCompletedComponent {
    @HostBinding("@enter") enter = true;
}
