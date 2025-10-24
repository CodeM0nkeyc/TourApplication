import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import {provideHttpClient} from "@angular/common/http";
import {provideRouter} from "@angular/router";
import {routes} from "./auth.routes";
import {provideAnimationsAsync} from "@angular/platform-browser/animations/async";

export const authConfig: ApplicationConfig = {
  providers: [
      provideZoneChangeDetection({ eventCoalescing: true }),
      provideHttpClient(),
      provideRouter(routes),
      provideAnimationsAsync()
  ]
};
