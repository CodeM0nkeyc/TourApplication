import {ChangeDetectionStrategy, Component, computed, inject, Signal} from '@angular/core';
import {FormBuilder, ReactiveFormsModule} from "@angular/forms";
import {ButtonComponent, DataInputComponent, ListInputComponent} from "shared/components";
import {AppCrossValidators, CoercionDirective} from "shared/validators";
import {ToursService} from "shared/services";
import type {TourDifficulty} from "shared/models";

@Component({
    selector: 'catalog-filter-panel',
    standalone: true,
    imports: [
        ButtonComponent,
        ListInputComponent,
        DataInputComponent,
        ReactiveFormsModule,
        CoercionDirective
    ],
    templateUrl: './filter-panel.component.html',
    styleUrl: './filter-panel.component.scss',
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class FilterPanelComponent {
    private readonly _toursService = inject(ToursService);
    private readonly _formBuilder = inject(FormBuilder);

    private _reloadOnReset = false;

    public readonly form = this._formBuilder.group({
        priceFrom: this._formBuilder.nonNullable.control<string | undefined>(undefined),
        priceTo: this._formBuilder.nonNullable.control<string | undefined>(undefined),
        countries: this._formBuilder.nonNullable.control<Map<string, any>>(new Map<string, any>()),
        difficulties: this._formBuilder.nonNullable.control<Map<string, any>>(new Map<string, any>()),
        remainingPlaces: this._formBuilder.nonNullable.control<string | undefined>(undefined),
        startDate: this._formBuilder.nonNullable.control<string | undefined>(undefined),
    }, {
        validators: [
            AppCrossValidators.range("priceFrom", "priceTo")
        ]
    });

    public readonly countries: Signal<Map<string, any>>;

    public readonly difficulties = new Map<TourDifficulty, number>([
        [ 'Easy', 1 ],
        [ 'Medium', 2 ],
        [ 'Hard', 3 ]
    ]);

    public constructor() {
        this._toursService.loadTourCountries();
        this.countries = computed(() => {
            const countryMapData = this._toursService.tourCountries()
                .map(country => [country, null] as [string, null])
                .sort((a, b) => a[0].localeCompare(b[0])) as [string, null][];
            return new Map(countryMapData);
        });
    }

    public async onSubmit(event: Event): Promise<void> {
        console.log(this.form.value);
        console.log(this.form.errors);

        if (this.form.dirty && this.form.valid) {
            const {
                countries,
                difficulties,
                remainingPlaces,
                startDate
            } = this.form.value;

            const priceFrom = this.form.value.priceFrom;
            const priceTo = this.form.value.priceTo;


            const EntityQuerySettings = {
                countries: countries!.size === 0
                    ? undefined : Array.from(countries!.keys()),
                difficulties: difficulties!.size === 0
                    ? undefined : Array.from(difficulties!.keys()) as TourDifficulty[],
                remainingPlaces: remainingPlaces,
                "priceSettings.lowerBound": Number.isNaN(priceFrom) ? undefined : priceFrom,
                "priceSettings.upperBound": Number.isNaN(priceTo) ? undefined : priceTo,
            }

            this._toursService.setFilterSettings(EntityQuerySettings);
            await this._toursService.loadToursAsync();

            this._reloadOnReset = true;
        }
    }

    public async onReset(event: Event): Promise<void> {
        this.form.reset();

        if (this._reloadOnReset) {
            await this._toursService.reloadToursAsync();
        }

        this._reloadOnReset = false;
    }
}
