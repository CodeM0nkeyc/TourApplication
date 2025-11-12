import {ChangeDetectionStrategy, Component} from '@angular/core';

@Component({
    selector: 'auth-welcome-section',
    standalone: true,
    imports: [],
    templateUrl: './welcome-section.component.html',
    styleUrl: './welcome-section.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class WelcomeSectionComponent {

}
