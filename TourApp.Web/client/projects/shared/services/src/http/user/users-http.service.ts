import { Injectable } from '@angular/core';
import {AppHttpService} from "../base/app-http-service";
import {HttpClient, HttpParams} from "@angular/common/http";
import {environment} from "shared";
import {catchError, Observable} from "rxjs";
import {ConfirmationCode, Credentials, RegistrationData} from "./users-http.model";
import {ApiResult} from "../base/app-http.model";

@Injectable({
    providedIn: 'root'
})
export class UsersHttpService extends AppHttpService {
    protected override apiUrl = environment.apiUrl + "/account";

    public constructor(httpClient: HttpClient) {
        super(httpClient);
    }

    public logIn(credentials: Credentials): Observable<ApiResult> {
        const signInRequest = this.httpClient.post<ApiResult>(`${this.apiUrl}/login`, credentials)
            .pipe(catchError(this.handleApiError));

        return signInRequest;
    }

    public registerUser(data: RegistrationData): Observable<ApiResult> {
        const registrationRequest = this.httpClient.post<ApiResult>(`${this.apiUrl}/register`, data)
            .pipe(catchError(this.handleApiError));

        return registrationRequest;
    }

    public userExists(email: string): Observable<ApiResult<boolean>> {
        const params = new HttpParams({
            fromString: `email=${email}`,
        });

        const existsRequest = this.httpClient.get<ApiResult<boolean>>(`${this.apiUrl}/exists`, {
            params: params
        }).pipe(catchError(this.handleApiError));

        return existsRequest;
    }

    public sendConfirmationCode(confirmationCode: ConfirmationCode): Observable<ApiResult> {
        const confirmationRequest =
            this.httpClient.post<ApiResult>(`${this.apiUrl}/confirm`, confirmationCode)
                .pipe(catchError(this.handleApiError));

        return confirmationRequest;
    }
}
