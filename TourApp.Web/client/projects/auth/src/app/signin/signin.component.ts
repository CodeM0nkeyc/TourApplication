import {ChangeDetectionStrategy, Component, HostBinding} from '@angular/core';
import {ButtonComponent, DataInputComponent} from "shared/components";
import {RouterLink} from "@angular/router";
import {enterTrigger} from "../auth.animations";

@Component({
    selector: 'auth-signin',
    standalone: true,
    imports: [
        DataInputComponent,
        ButtonComponent,
        RouterLink
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
}
