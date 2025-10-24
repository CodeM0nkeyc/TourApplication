import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";

export abstract class AppHttpService {
    protected readonly httpClient: HttpClient;

    protected constructor(httpClient: HttpClient) {
        this.httpClient = httpClient;
    }

    protected getAsText(reqParams: {
        url: string,
        headers?: HttpHeaders,
        params?: HttpParams,
        postBody?: any }): Observable<string> {

        const opts = {
            headers: reqParams.headers,
            params: reqParams.params
        }

        if (reqParams.postBody) {
            return this.httpClient.post(reqParams.url, reqParams.postBody, {
                responseType: "text",
                ...opts
            })
        }

        return this.httpClient.get(reqParams.url, {
            responseType: "text",
            ...opts
        });
    }

    protected getAsObject<T>(reqParams: {
        url: string,
        headers?: HttpHeaders,
        params?: HttpParams,
        postBody?: any }): Observable<T> {

        const opts = {
            headers: reqParams.headers,
            params: reqParams.params
        }

        if (reqParams.postBody) {
            return this.httpClient.post<T>(reqParams.url, reqParams.postBody, {
                responseType: "json",
                ...opts
            })
        }

        return this.httpClient.get<T>(reqParams.url, {
            responseType: "json",
            headers: reqParams.headers,
            params: reqParams.params
        });
    }
}
