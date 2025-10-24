import {ChangeDetectionStrategy, Component, input} from '@angular/core';
import {TourCardFrontVM} from "./tour-card-front.model";
import {DatePipe} from "@angular/common";

@Component({
    selector: 'home-tour-card-front',
    standalone: true,
    imports: [
        DatePipe
    ],
    templateUrl: './tour-card-front.component.html',
    styleUrl: './tour-card-front.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class TourCardFrontComponent {
    public readonly tourCardFrontVM = input.required<TourCardFrontVM>();
}
