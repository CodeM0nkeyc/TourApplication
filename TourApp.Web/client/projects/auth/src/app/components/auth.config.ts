import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import {provideHttpClient, withInterceptors} from "@angular/common/http";
import {provideRouter, withComponentInputBinding} from "@angular/router";
import {routes} from "./auth.routes";
import {provideAnimationsAsync} from "@angular/platform-browser/animations/async";
import {apiErrorInterceptor, provideGlobalErrorHandler} from "shared/services";

export const authConfig: ApplicationConfig = {
  providers: [
      provideZoneChangeDetection({ eventCoalescing: true }),
      provideHttpClient(withInterceptors([apiErrorInterceptor])),
      provideRouter(routes, withComponentInputBinding()),
      provideAnimationsAsync(),
      provideGlobalErrorHandler()
  ]
};
