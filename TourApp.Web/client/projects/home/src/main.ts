import { bootstrapApplication } from '@angular/platform-browser';
import { homeConfig } from './app/home.config';
import { HomeComponent } from './app/home.component';

bootstrapApplication(HomeComponent, homeConfig)
  .catch((err) => console.error(err));
