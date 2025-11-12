import {inject, Injectable, Signal, signal} from '@angular/core';
import {UsersHttpService} from "../http/user/users-http.service";
import {ConfirmationCode, Credentials, RegistrationData} from "../http/user/users-http.model";
import {ApiResult} from "../http/base/app-http.model";
import {lastValueFrom} from "rxjs";
import {Notify} from "shared/utilities";

@Injectable({
  providedIn: 'root'
})
export class UsersService {
    private readonly _usersHttpService = inject(UsersHttpService);

    private readonly _isLoading = signal<boolean>(false);

    public get isLoading(): Signal<boolean> {
        return this._isLoading.asReadonly();
    }

    @Notify("_isLoading")
    public async logInAsync(credentials: Credentials): Promise<ApiResult> {
        const logInResult = lastValueFrom(this._usersHttpService.logIn(credentials));

        return logInResult;
    }

    @Notify("_isLoading")
    public async registerAsync(registerData: RegistrationData): Promise<ApiResult> {
        const registerResult = lastValueFrom(this._usersHttpService.registerUser(registerData));

        return registerResult;
    }

    @Notify("_isLoading")
    public async userExistsAsync(email: string): Promise<ApiResult<boolean>> {
        const existsResult = lastValueFrom(this._usersHttpService.userExists(email));

        return existsResult;
    }

    @Notify("_isLoading")
    public async confirmEmailAsync(confirmationCode: ConfirmationCode): Promise<ApiResult> {
        const confirmationResult = lastValueFrom(this._usersHttpService.sendConfirmationCode(confirmationCode));

        return confirmationResult;
    }
}
