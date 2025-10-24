import {Component} from '@angular/core';
import {WelcomeSectionComponent} from "./welcome-section/welcome-section.component";
import {RouterOutlet} from "@angular/router";

@Component({
    selector: 'auth-root',
    standalone: true,
    imports: [
        WelcomeSectionComponent,
        RouterOutlet
    ],
    templateUrl: './auth.component.html',
    styleUrl: './auth.component.scss'
})
export class AuthComponent {

}
