import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {environment} from "shared";
import {Observable, of} from "rxjs";
import {ApiResult} from "./app-http.model";

export abstract class AppHttpService {
    protected readonly httpClient: HttpClient;
    protected readonly apiUrl = environment.apiUrl;

    protected constructor(httpClient: HttpClient) {
        this.httpClient = httpClient;
    }

    protected handleApiError(err: HttpErrorResponse): Observable<ApiResult> {
        const apiError = err.error as ApiResult;

        if (Object.hasOwn(apiError, "isSuccess")) {
            return of(apiError);
        }

        throw err;
    }
}
