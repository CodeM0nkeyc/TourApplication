import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import {provideHttpClient, withInterceptors} from "@angular/common/http";
import {apiErrorInterceptor, provideGlobalErrorHandler} from "shared/services";
import {provideAnimationsAsync} from "@angular/platform-browser/animations/async";

export const catalogConfig: ApplicationConfig = {
  providers: [
      provideZoneChangeDetection({ eventCoalescing: true }),
      provideHttpClient(withInterceptors([apiErrorInterceptor])),
      provideGlobalErrorHandler(),
      provideAnimationsAsync()
  ]
};
