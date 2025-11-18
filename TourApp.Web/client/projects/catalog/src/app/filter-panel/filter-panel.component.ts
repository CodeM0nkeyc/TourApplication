import {ChangeDetectionStrategy, Component, computed, inject, OnInit, Signal} from '@angular/core';
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
export class FilterPanelComponent implements OnInit {
    private readonly _toursService = inject(ToursService);
    private readonly _formBuilder = inject(FormBuilder);

    private _reloadOnReset = false;

    public readonly form = this._formBuilder.group({
        priceFrom: this._formBuilder.nonNullable.control<string | undefined>(undefined),
        priceTo: this._formBuilder.nonNullable.control<string | undefined>(undefined),
        countries: this._formBuilder.nonNullable.control<Map<string, {}>>(new Map<string, {}>()),
        difficulties: this._formBuilder.nonNullable.control<Map<string, number>>(new Map<string, number>()),
        remainingPlaces: this._formBuilder.nonNullable.control<string | undefined>(undefined),
        startDate: this._formBuilder.nonNullable.control<string | undefined>(undefined),
    }, {
        validators: [
            AppCrossValidators.range("priceFrom", "priceTo")
        ]
    });

    public readonly countries: Signal<Map<string, {}>>;

    public readonly difficulties = new Map<TourDifficulty, number>([
        [ 'Easy', 1 ],
        [ 'Medium', 2 ],
        [ 'Hard', 3 ]
    ]);

    public constructor() {
        this.countries = computed(() => {
            const countryMapData = this._toursService.tourCountries()
                .map(country => [country, {}] as [string, {}])
                .sort((a, b) => a[0].localeCompare(b[0])) as [string, {}][];
            return new Map(countryMapData);
        });
    }

    public async ngOnInit(): Promise<void> {
        await this._toursService.loadTourCountriesAsync();
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
