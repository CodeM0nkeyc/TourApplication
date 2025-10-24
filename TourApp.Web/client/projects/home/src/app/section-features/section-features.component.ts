import {ChangeDetectionStrategy, Component} from '@angular/core';
import {FeatureBoxComponent} from "./feature-box/feature-box.component";

@Component({
    selector: 'home-section-features',
    standalone: true,
    imports: [
        FeatureBoxComponent
    ],
    templateUrl: './section-features.component.html',
    styleUrl: './section-features.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class SectionFeaturesComponent {

}
