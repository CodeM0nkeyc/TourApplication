import {Component, inject, Signal} from '@angular/core';
import {WelcomeSectionComponent} from "./welcome-section/welcome-section.component";
import {RouterOutlet} from "@angular/router";
import {SpinnerComponent} from "shared/components";
import {UsersService} from "shared/services";
import {GlobalErrorComponent} from "shared/components";

@Component({
    selector: 'auth-root',
    standalone: true,
    imports: [
        WelcomeSectionComponent,
        RouterOutlet,
        SpinnerComponent,
        GlobalErrorComponent
    ],
    templateUrl: './auth.component.html',
    styleUrl: './auth.component.scss'
})
export class AuthComponent {
    private readonly _usersService = inject(UsersService)

    public readonly isWaitingResponse: Signal<boolean>;

    public constructor() {
        this.isWaitingResponse = this._usersService.isLoading;
    }
}
