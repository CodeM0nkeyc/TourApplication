import {ChangeDetectionStrategy, Component, input} from '@angular/core';
import { ButtonComponent } from 'shared/components';

@Component({
    selector: 'home-tour-card-back',
    standalone: true,
    imports: [
        ButtonComponent
    ],
    templateUrl: './tour-card-back.component.html',
    styleUrl: './tour-card-back.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class TourCardBackComponent {
    public readonly price = input.required<number>();
}
