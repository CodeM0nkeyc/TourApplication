import {ChangeDetectionStrategy, Component} from '@angular/core';
import {ButtonComponent} from "shared/components"

@Component({
    selector: 'home-section-intro',
    standalone: true,
    imports: [
        ButtonComponent
    ],
    templateUrl: './section-intro.component.html',
    styleUrl: './section-intro.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class SectionIntroComponent {

}
