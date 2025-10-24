import {ChangeDetectionStrategy, Component} from '@angular/core';
import {ButtonComponent, SectionHeadingComponent} from "shared/components";

@Component({
    selector: 'home-section-about',
    standalone: true,
    imports: [
        SectionHeadingComponent,
        ButtonComponent
    ],
    templateUrl: './section-about.component.html',
    styleUrl: './section-about.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class SectionAboutComponent {

}
