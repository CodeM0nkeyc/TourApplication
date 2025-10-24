import {ChangeDetectionStrategy, Component, input} from '@angular/core';
import type {TourCardVM} from 'shared/models';
import {TourCardFrontComponent} from "./tour-card-front/tour-card-front.component";
import {TourCardBackComponent} from "./tour-card-back/tour-card-back.component";

@Component({
    selector: 'home-tour-card',
    standalone: true,
    templateUrl: './tour-card.component.html',
    styleUrl: './tour-card.component.scss',
    imports: [
        TourCardFrontComponent,
        TourCardBackComponent
    ],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class TourCardComponent {
    public readonly TourCardVM = input.required<TourCardVM>();
}
