import {Provider, Signal, signal} from '@angular/core';
import type {TourCardVM, TourQuerySettings} from "shared/models";
import {ToursHttpService} from "../http/tour/tours-http.service";
import {lastValueFrom} from "rxjs";

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

    public constructor(private readonly _tourHttpService: ToursHttpService, pageSize: number) {
        this._pageSize = pageSize;
    }

    public setFilterSettings(EntityQuerySettings: TourQuerySettings): void {
        this._filterSettings = EntityQuerySettings;
    }

    public async loadToursAsync(preserveOld: boolean = false): Promise<void> {
        if (!preserveOld) {
            this._viewPageIndex = 0;
            this._requestPageIndex = 0;
        }

        const futureToursLength = this._pageSize * (this._viewPageIndex + 1);

        if (!preserveOld || this._loadedTours().length - futureToursLength <= 0) {
            this._isLoading.set(true);
            this._filterSettings.pageIndex = this._requestPageIndex;

            let tours: TourCardVM[] = [];

            try {
                tours = await lastValueFrom(this._tourHttpService.getTours(this._filterSettings));
            }
            catch (error) {
                console.log(error);
            }
            finally {
                this._isLoading.set(false);
            }

            this._loadedTours.update(oldArr =>
                preserveOld ? [...oldArr, ...tours] : tours);

            this._isLastDataLoaded.set(this._loadedTours().length - futureToursLength <= 0)
            this._requestPageIndex++;
        }

        this._tours.set(this._loadedTours()
            .slice(0, futureToursLength));

        this._viewPageIndex++;
    }

    public async reloadToursAsync(EntityQuerySettings?: TourQuerySettings): Promise<void> {
        this.setFilterSettings(EntityQuerySettings ?? {});
        await this.loadToursAsync();
    }

    public loadTourCountries(): void {
        this._isLoading.set(true);

        this._tourHttpService.getTourCountries()
            .subscribe({
                next: tourCountries => {
                    this._tourCountries.set(tourCountries);
                },
                error: err => {
                    this._isLoading.set(false);
                },
                complete: () => {
                    this._isLoading.set(false);
                }
            });
    }
}
