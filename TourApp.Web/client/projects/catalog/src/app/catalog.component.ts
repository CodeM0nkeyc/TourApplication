import {Component, inject, Signal} from '@angular/core';
import {TourCardComponent} from "./tour-card/tour-card.component";
import {FilterPanelComponent} from "./filter-panel/filter-panel.component";
import {provideToursService, ToursService} from "shared/services";
import type {TourCardVM} from "shared/models";
import {
    ButtonComponent,
    FooterComponent,
    HeaderComponent,
    SectionHeadingComponent,
    SpinnerComponent
} from "shared/components";

@Component({
    selector: 'catalog-root',
    standalone: true,
    templateUrl: './catalog.component.html',
    imports: [
        TourCardComponent,
        FilterPanelComponent,
        HeaderComponent,
        SpinnerComponent,
        SectionHeadingComponent,
        FooterComponent,
        ButtonComponent,
    ],
    styleUrl: './catalog.component.scss',
    providers: [provideToursService(6)]
})
export class CatalogComponent {
    private readonly _toursService = inject(ToursService);

    public readonly tours: Signal<Array<TourCardVM>>;
    public readonly isLoading: Signal<boolean>;
    public readonly isLastToursLoaded: Signal<boolean>;

    public constructor() {
        this._toursService.loadToursAsync();
        this.tours = this._toursService.tours;
        this.isLoading = this._toursService.isLoading;
        this.isLastToursLoaded = this._toursService.isLastDataLoaded;
    }

    public async onLoadMore(event: Event): Promise<void> {
        await this._toursService.loadToursAsync(true);
    }
}
