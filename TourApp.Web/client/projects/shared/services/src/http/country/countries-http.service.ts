import {Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AppHttpService} from "../base/app-http-service";
import {map, Observable} from "rxjs";
import {environment} from "shared";
import {Cities, Countries, States} from "./countries-http.model";

@Injectable({
  providedIn: 'root'
})
export class CountriesHttpService extends AppHttpService {
    protected override apiUrl = environment.countriesApiUrl;

    public constructor(httpClient: HttpClient) {
        super(httpClient);
    }

    public getCountriesWithDialCodes(): Observable<{ country: string, dialCode: string }[]> {
        return this.httpClient.get<Countries>(`${this.apiUrl}/codes`)
            .pipe(map(response => response.data.map(
                entity => {
                    return {
                        country: entity.name,
                        dialCode: entity.dial_code
                    }
                })));
    }

    public getRegions(country: string): Observable<string[]> {
        return this.httpClient.post<States>(`${this.apiUrl}/states`, { country: country })
            .pipe(map(response => response.data.states.map(entity => entity.name)));
    }

    public getCities(country: string, region?: string): Observable<string[]> {
        return this.httpClient.post<Cities>(`${this.apiUrl}/cities`, {
            country: country,
            state: region
        }).pipe(map(response => response.data));
    }
}
