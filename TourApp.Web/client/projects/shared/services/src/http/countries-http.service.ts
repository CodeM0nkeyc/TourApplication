import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AppHttpService} from "./base/app-http-service";
import {map, Observable} from "rxjs";
import {environment} from "shared";
import {Cities, Countries, States} from "./countries-http.model";

@Injectable({
  providedIn: 'root'
})
export class CountriesHttpService extends AppHttpService {
    private readonly _apiUrl = environment.countriesApiUrl;

    public constructor(httpClient: HttpClient) {
        super(httpClient);
    }

    public getCountriesWithDialCodes(): Observable<{ country: string, dialCode: string }[]> {
        return this.getAsObject<Countries>(
            {
                url: `${this._apiUrl}/codes`
            })
            .pipe(map(response => response.data.map(
                entity => {
                    return {
                        country: entity.name,
                        dialCode: entity.dial_code
                    }
                })));
    }

    public getRegions(country: string): Observable<string[]> {
        return this.getAsObject<States>(
            {
                url: `${this._apiUrl}/states`,
                postBody: { country: country }
            })
            .pipe(map(response => response.data.states.map(entity => entity.name)));
    }

    public getCities(country: string, region?: string): Observable<string[]> {
        return this.getAsObject<Cities>(
            {
                url: `${this._apiUrl}/cities`,
                postBody: {
                    country: country,
                    state: region
                }
            })
            .pipe(map(response => response.data));
    }
}
