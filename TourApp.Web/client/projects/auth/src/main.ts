import { bootstrapApplication } from '@angular/platform-browser';
import { authConfig } from './app/auth.config';
import { AuthComponent } from './app/auth.component';

bootstrapApplication(AuthComponent, authConfig)
  .catch((err) => console.error(err));
