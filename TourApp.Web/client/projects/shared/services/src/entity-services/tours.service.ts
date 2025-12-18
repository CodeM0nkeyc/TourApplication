import {Provider, Signal, signal} from '@angular/core';
import {FriendlyError, TourCardVM, TourQuerySettings} from "shared/models";
import {ToursHttpService} from "../http/tour/tours-http.service";
import {lastValueFrom} from "rxjs";
import {Notify} from "shared/utilities";

export function provideToursService(pageSize: number = 24): Provider {
    return {
        provide: ToursService,
        useFactory: (toursHttpService: ToursHttpService) => new ToursService(toursHttpService, pageSize),
        deps: [ToursHttpService]
    };
}

export class ToursService {
    private _filterSettings: TourQuerySettings = {};

    private _viewPageIndex = 0;
    private _requestPageIndex = 0;

    private readonly _pageSize: number;

    private readonly _tourHttpService: ToursHttpService;

    private readonly _loadedTours = signal<TourCardVM[]>([]);
    private readonly _tours = signal<TourCardVM[]>([]);
    private readonly _tourCountries = signal<string[]>([]);

    private readonly _isLoading = signal<boolean>(false);

    private readonly _isLastDataLoaded = signal<boolean>(false);

    public get isLoading(): Signal<boolean> {
        return this._isLoading.asReadonly();
    }

    public get tours(): Signal<TourCardVM[]> {
        return this._tours.asReadonly();
    }

    public get tourCountries(): Signal<string[]> {
        return this._tourCountries.asReadonly();
    }

    public get isLastDataLoaded(): Signal<boolean> {
        return this._isLastDataLoaded.asReadonly();
    }

    public constructor(tourHttpService: ToursHttpService, pageSize: number) {
        this._tourHttpService = tourHttpService;
        this._pageSize = pageSize;
    }

    @Notify("_isLoading")
    private async _loadToursAsync(tourQuerySettings: TourQuerySettings): Promise<TourCardVM[]> {
        try {
            const result = await lastValueFrom(this._tourHttpService.getTours(this._filterSettings));
            return result.data;
        }
        catch (e: any) {
            throw new FriendlyError(
                "Failed to load tours. Please, check your internet connection or try later", e);
        }
    }

    public setFilterSettings(tourQuerySettings: TourQuerySettings): void {
        this._filterSettings = tourQuerySettings;
    }

    public async loadToursAsync(preserveOld: boolean = false): Promise<void> {
        if (!preserveOld) {
            this._viewPageIndex = 0;
            this._requestPageIndex = 0;
        }

        const futurePageLength = this._pageSize * (this._viewPageIndex + 1);

        if (!preserveOld || this._loadedTours().length - futurePageLength <= 0) {
            this._filterSettings.pageIndex = this._requestPageIndex;

            let tours = await this._loadToursAsync(this._filterSettings);

            this._loadedTours.update(oldArr =>
                preserveOld ? [...oldArr, ...tours] : tours);

            this._isLastDataLoaded.set(this._loadedTours().length - futurePageLength <= 0)

            this._requestPageIndex++;
        }

        this._tours.set(this._loadedTours()
            .slice(0, futurePageLength));

        this._viewPageIndex++;
    }

    public async reloadToursAsync(EntityQuerySettings?: TourQuerySettings): Promise<void> {
        this.setFilterSettings(EntityQuerySettings ?? {});
        await this.loadToursAsync();
    }

    @Notify("_isLoading")
    public async loadTourCountriesAsync(): Promise<void> {
        try {
            const result = await lastValueFrom(this._tourHttpService.getTourCountries());
            this._tourCountries.set(result.data);
        }
        catch (e: any) {
            throw new FriendlyError("Failed to load tours countries", e);
        }
    }
}
