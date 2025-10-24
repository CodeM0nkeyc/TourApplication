import {HttpParams} from "@angular/common/http";
import {EntityQuerySettings} from "./types";

export interface IQuerySettingsParser {
    getQueryFromSettings(settings?: EntityQuerySettings): HttpParams | undefined;
}
