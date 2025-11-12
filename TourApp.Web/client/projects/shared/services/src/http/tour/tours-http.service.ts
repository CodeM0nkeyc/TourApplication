import {inject, Injectable} from '@angular/core';
import type {TourCardVM, TourQuerySettings} from "shared/models";
import {QuerySettingsParserService} from "../../parsers/query-settings-parser.service";
import {map, Observable} from "rxjs";
import {AppHttpService} from "../base/app-http-service";
import {HttpClient} from "@angular/common/http";
import {environment} from "shared";

@Injectable({
    providedIn: 'root'
})
export class ToursHttpService extends AppHttpService {
    private readonly _queryParser = inject(QuerySettingsParserService);

    public constructor(httpClient: HttpClient) {
        super(httpClient);
    }

    public getTours(EntityQuerySettings?: TourQuerySettings): Observable<TourCardVM[]> {
        const params = this._queryParser.getQueryFromSettings(EntityQuerySettings);
        const response = this.httpClient.get(`${this.apiUrl}/tours`, {
            responseType: "text",
            params: params
        }).pipe(map<string, TourCardVM[]>(response =>
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

        return response;
    }

    public getTourCountries(): Observable<string[]> {
        const response = this.httpClient.get<string[]>(`${this.apiUrl}/tours/countries`);

        return response;
    }
}
