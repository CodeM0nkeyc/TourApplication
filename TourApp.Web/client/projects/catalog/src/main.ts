import { bootstrapApplication } from '@angular/platform-browser';
import { catalogConfig } from './app/catalog.config';
import { CatalogComponent } from './app/catalog.component';

bootstrapApplication(CatalogComponent, catalogConfig)
  .catch((err) => console.error(err));
