import {inject, Injectable} from '@angular/core';
import type {TourCardVM, TourQuerySettings} from "shared/models";
import {QuerySettingsParserService} from "../parsers/query-settings-parser.service";
import {map, Observable} from "rxjs";
import {AppHttpService} from "./base/app-http-service";
import {HttpClient} from "@angular/common/http";
import {environment} from "shared";

@Injectable({
    providedIn: 'root'
})
export class ToursHttpService extends AppHttpService {
    private readonly _queryParser = inject(QuerySettingsParserService);
    private readonly _apiUrl = environment.apiUrl;


    public constructor(httpClient: HttpClient) {
        super(httpClient);
    }

    public getTours(EntityQuerySettings?: TourQuerySettings): Observable<TourCardVM[]> {
        const params = this._queryParser.getQueryFromSettings(EntityQuerySettings);

        return this.getAsText({ url: `${this._apiUrl}/tours`, params: params })
            .pipe(map<string, TourCardVM[]>(response =>
                JSON.parse(response, (key, value) => {

                    switch (key.toLowerCase()) {
                        case "startdate":
                            return new Date(value);
                        case "imagesrc":
                            return `https://localhost:7136/img/${value}`;
                    }

                    return value;
                })
            ));
    }

    public getTourCountries(): Observable<string[]> {
        return this.getAsObject<string[]>({ url: `${this._apiUrl}/tours/countries` });
    }
}
