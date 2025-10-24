import {ChangeDetectionStrategy, Component, input} from '@angular/core';
import {ButtonComponent} from "shared/components";
import type {TourCardVM} from "shared/models";
import {DatePipe} from "@angular/common";

@Component({
    selector: 'catalog-tour-card',
    standalone: true,
    imports: [
        ButtonComponent,
        DatePipe
    ],
    templateUrl: './tour-card.component.html',
    styleUrl: './tour-card.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class TourCardComponent {
    public readonly tourCardVM = input.required<TourCardVM>();
}
