import {inject, Injectable, signal} from '@angular/core';
import {CountriesHttpService} from "../http/country/countries-http.service";
import {lastValueFrom} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class CountriesService {
    private readonly _countriesHttpService = inject(CountriesHttpService);

    private readonly _countries = signal<{ country: string, dialCode: string }[]>([]);
    private readonly _regions = signal<string[]>([]);
    private readonly _cities = signal<string[]>([]);

    public get countries() {
        return this._countries.asReadonly();
    }

    public get regions() {
        return this._regions.asReadonly();
    }

    public get cities() {
        return this._cities.asReadonly();
    }

    public async loadCountriesAsync(): Promise<void> {
        const countries = await lastValueFrom(this._countriesHttpService.getCountriesWithDialCodes());
        this._countries.set(countries);
    }

    public async loadRegionsAsync(country: string): Promise<void> {
        const regions = await lastValueFrom(this._countriesHttpService.getRegions(country));
        this._regions.set(regions);
    }

    public async loadCitiesAsync(country: string, region?: string): Promise<void> {
        const cities = await lastValueFrom(this._countriesHttpService.getCities(country, region));
        this._cities.set(cities);
    }
}
