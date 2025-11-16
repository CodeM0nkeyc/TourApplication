import {ErrorHandler, Injectable, Provider} from '@angular/core';
import {BehaviorSubject} from "rxjs";
import {FriendlyError} from "shared/models";

export function provideGlobalErrorHandler(): Provider {
    return {
        provide: ErrorHandler,
        useExisting: GlobalErrorHandler
    }
}

@Injectable({
    providedIn: 'root'
})
export class GlobalErrorHandler extends ErrorHandler {
    public readonly errors = new BehaviorSubject<string>("");

    public override handleError(error: any): void {
        if (error instanceof FriendlyError) {
            this.errors.next(error.message);
        }
        else {
            super.handleError(error);
        }
    }
}
