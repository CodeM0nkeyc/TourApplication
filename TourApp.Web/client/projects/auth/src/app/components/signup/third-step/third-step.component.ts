import {ChangeDetectionStrategy, Component, computed, inject, signal} from '@angular/core';
import {ButtonComponent, ListInputComponent, type ListInputValue, ValidationMessageComponent} from "shared/components";
import {FormBuilder, FormsModule, ReactiveFormsModule} from "@angular/forms";
import {StepComponentBase} from "../step-component-base";
import {enterTrigger} from "shared/animations";
import {CountriesService} from "shared/services";
import {AppValidators} from "shared/validators";
import {FriendlyError} from "shared/models";

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
        country: this._formBuilder.nonNullable.control<ListInputValue>(["", {}], [
            AppValidators.required("Country", (value) => !value[0])
        ]),
        region: this._formBuilder.nonNullable.control<ListInputValue>(["", {}]),
        city: this._formBuilder.nonNullable.control<ListInputValue>(["", {}])
    });

    public readonly countries = computed<Map<string, {}>>(() => {
            return new Map(this._countriesService.countries()
                .map(countryEntry => [countryEntry.country, {}]));
        });

    public readonly regions = computed<Map<string, {}>>(() => {
        return new Map(this._countriesService.regions()
            .map(region => [region, {}]));
    });

    public readonly cities = computed<Map<string, {}>>(() => {
        return new Map(this._countriesService.cities()
            .map(city => [city, {}]));
    });

    public readonly regionsAvailable = signal<boolean>(false);
    public readonly citiesAvailable = signal<boolean>(false);

    public constructor() {
        super("signup/step4");
    }

    public override async ngOnInit(): Promise<void> {
        try {
            await this._countriesService.loadCountriesAsync();
        }
        catch (e: any) {
            throw new FriendlyError(
                "Failed to load countries. Please, check your internet connection or try later", e);
        }

        this.form.controls.country.valueChanges
            .subscribe(async country => {
                if (country[0] !== "") {
                    try {
                        await this._countriesService.loadRegionsAsync(country[0]);
                    }
                    catch(e: any) {
                        throw new FriendlyError(
                            "Failed to load regions. Please, check your internet connection or try later", e);
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
                    this.form.controls.region.setValue(["", {}]);
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
                    catch(e: any) {
                        throw new FriendlyError(
                            "Failed to load cities. Please, check your internet connection or try later", e);
                    }
                    finally {
                        this.citiesAvailable.set(this.cities().size > 0);
                    }
                }
                else {
                    this.form.controls.city.setValue(["", {}]);
                    this.citiesAvailable.set(false);
                }
            });

        super.ngOnInit();
    }

    protected override getSavedData(key: string): any {
        const savedData = super.getSavedData(key);

        return savedData ? [savedData, {}] : null;
    }

    protected override saveData() {
        sessionStorage.setItem("country", this.form.value.country![0]);
        sessionStorage.setItem("region", this.form.value.region![0]);
        sessionStorage.setItem("city", this.form.value.city![0]);
    }
}
