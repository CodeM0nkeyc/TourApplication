import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import {provideHttpClient} from "@angular/common/http";
import {provideGlobalErrorHandler} from "shared/services";
import {provideAnimationsAsync} from "@angular/platform-browser/animations/async";

export const homeConfig: ApplicationConfig = {
  providers: [
      provideZoneChangeDetection({ eventCoalescing: true }),
      provideHttpClient(),
      provideGlobalErrorHandler(),
      provideAnimationsAsync()
  ]
};
