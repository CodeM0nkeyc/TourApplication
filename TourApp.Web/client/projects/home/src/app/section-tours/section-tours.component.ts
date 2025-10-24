import {ChangeDetectionStrategy, Component, inject, Signal, signal} from '@angular/core';
import {TourCardComponent} from "./tour-card/tour-card.component";
import {TourCardFrontComponent} from "./tour-card/tour-card-front/tour-card-front.component";
import {TourCardBackComponent} from "./tour-card/tour-card-back/tour-card-back.component";
import {ButtonComponent, SectionHeadingComponent, SpinnerComponent} from "shared/components";
import {provideToursService, ToursService} from "shared/services";
import type {TourCardVM} from "shared/models";

@Component({
    selector: 'home-section-tours',
    standalone: true,
    templateUrl: './section-tours.component.html',
    imports: [
        TourCardComponent,
        SectionHeadingComponent,
        ButtonComponent,
        SpinnerComponent
    ],
    styleUrl: './section-tours.component.scss',
    providers: [provideToursService()],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class SectionToursComponent {
    private readonly _toursService = inject(ToursService);

    public readonly tours: Signal<TourCardVM[]> = signal<TourCardVM[]>([])

    public constructor() {
        this._toursService.loadToursAsync();
        this.tours = this._toursService.tours;
    }

    protected readonly Math = Math;
}
