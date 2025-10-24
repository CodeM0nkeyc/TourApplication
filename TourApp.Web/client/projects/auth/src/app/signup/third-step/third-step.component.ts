import {ChangeDetectionStrategy, Component, computed, inject, signal} from '@angular/core';
import {ButtonComponent, ListInputComponent, type ListInputValue, ValidationMessageComponent} from "shared/components";
import {FormBuilder, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {StepComponentBase} from "../step-component-base";
import {enterTrigger} from "../../auth.animations";
import {CountriesService} from "shared/services";
import {AppValidators} from "shared/validators";

@Component({
    selector: 'auth-third-step',
    standalone: true,
    imports: [
        ButtonComponent,
        FormsModule,
        ListInputComponent,
        ReactiveFormsModule,
        ValidationMessageComponent
    ],
    templateUrl: './third-step.component.html',
    styleUrl: './third-step.component.scss',
    animations: [
        enterTrigger
    ],
    changeDetection: ChangeDetectionStrategy.OnPush
})
export class ThirdStepComponent extends StepComponentBase {
    private readonly _formBuilder = inject(FormBuilder);
    private readonly _countriesService = inject(CountriesService);

    public override readonly form = this._formBuilder.group({
        country: this._formBuilder.nonNullable.control<ListInputValue>(["", ""], [
            AppValidators.required("Country", (value) => !value[0])
        ]),
        region: this._formBuilder.nonNullable.control<ListInputValue>(["", null]),
        city: this._formBuilder.nonNullable.control<ListInputValue>(["", null])
    });

    public readonly countries = computed<Map<string, string>>(() => {
            return new Map(this._countriesService.countries()
                .map(countryEntry => [countryEntry.country, countryEntry.dialCode]))
        });

    public readonly regions = computed<Map<string, null>>(() => {
        return new Map(this._countriesService.regions()
            .map(region => [region, null]))
    });

    public readonly cities = computed<Map<string, null>>(() => {
        return new Map(this._countriesService.cities()
            .map(city => [city, null]))
    });

    public readonly regionsAvailable = signal<boolean>(false);
    public readonly citiesAvailable = signal<boolean>(false);

    public constructor() {
        super("signup/step4");

        (async () => {
            await this._countriesService.loadCountriesAsync();

            this.form.controls.country.valueChanges
                .subscribe(async country => {
                    if (country[0] !== "") {
                        try{
                            await this._countriesService.loadRegionsAsync(country[0]);
                        }
                        finally {
                            this.regionsAvailable.set(this.regions().size > 0);
                        }

                        if (!this.regionsAvailable()) {
                            await this._countriesService.loadCitiesAsync(country[0]);
                            this.citiesAvailable.set(this.cities().size > 0)
                        }
                    }
                    else {
                        this.form.controls.region.setValue(["", null]);
                        this.regionsAvailable.set(false);
                    }
                });

            this.form.controls.region.valueChanges
                .subscribe(async region => {
                    if (region[0] !== "") {
                        const country = this.form.controls.country.value[0];

                        try {
                            await this._countriesService.loadCitiesAsync(country, region[0]);
                        }
                        finally {
                            this.citiesAvailable.set(this.cities().size > 0);
                        }
                    }
                    else {
                        this.form.controls.city.setValue(["", null]);
                        this.citiesAvailable.set(false);
                    }
                });
        })();
    }
}
