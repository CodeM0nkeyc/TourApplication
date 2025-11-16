import {HttpErrorResponse, HttpEvent, HttpInterceptorFn, HttpResponse} from '@angular/common/http';
import {catchError, Observable, of} from "rxjs";
import {FriendlyError} from "shared/models";

function handleApiError(err: HttpErrorResponse): Observable<HttpEvent<unknown>> {
    const apiError = err.error;

    if (apiError && Object.hasOwn(apiError, "isSuccess")
            && Object.hasOwn(apiError, "errors")) {
        const response = new HttpResponse({
            body: apiError,
            url: err.url ?? undefined,
            status: 200,
            headers: err.headers,
            statusText: "OK",
        });

        return of(response);
    }

    if (err.status === 0) {
        throw new FriendlyError("Server is unavailable. Please, check your connection or try later.", err);
    }

    if (err.status >= 500) {
        throw new FriendlyError("Something went wrong. Please, try later.", err);
    }

    throw err;
}

export const apiErrorInterceptor: HttpInterceptorFn = (req, next) => {
    return next(req).pipe(catchError(handleApiError));
};
