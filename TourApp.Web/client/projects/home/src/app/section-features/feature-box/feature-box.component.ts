import {ChangeDetectionStrategy, Component} from '@angular/core';

@Component({
    selector: 'home-feature-box',
    standalone: true,
    imports: [],
    templateUrl: './feature-box.component.html',
    styleUrl: './feature-box.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class FeatureBoxComponent {

}
