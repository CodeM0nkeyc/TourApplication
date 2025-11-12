import { bootstrapApplication } from '@angular/platform-browser';
import { authConfig } from './app/components/auth.config';
import { AuthComponent } from './app/components/auth.component';

bootstrapApplication(AuthComponent, authConfig)
  .catch((err) => console.error(err));
