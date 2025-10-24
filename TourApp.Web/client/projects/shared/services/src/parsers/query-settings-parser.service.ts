import {Injectable} from '@angular/core';
import {HttpParams} from "@angular/common/http";
import type {IQuerySettingsParser} from "./common/iquery-settings-parser";
import type {EntityQuerySettings} from "./common/types";

@Injectable({
    providedIn: 'root'
})
export class QuerySettingsParserService implements IQuerySettingsParser{
    public getQueryFromSettings(settings?: EntityQuerySettings): HttpParams | undefined {
        let params = new HttpParams();

        if (!settings) {
            return undefined;
        }

        for (let key in settings) {
            let value = settings[key];

            if (!value) {
                continue;
            }

            if (Array.isArray(value)) {
                for (let i = 0; i < value.length; i++) {
                    params = params.append(`${key}[${i}]`, value[i]);
                }
            }
            else {
                params = params.append(key, value);
            }
        }

        return params;
    }
}
