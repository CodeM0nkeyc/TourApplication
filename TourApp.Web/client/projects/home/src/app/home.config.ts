import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import {provideHttpClient} from "@angular/common/http";

export const homeConfig: ApplicationConfig = {
  providers: [provideZoneChangeDetection({ eventCoalescing: true }), provideHttpClient()]
};
