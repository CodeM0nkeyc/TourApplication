import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import {provideHttpClient} from "@angular/common/http";

export const catalogConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }), provideHttpClient()]
};
