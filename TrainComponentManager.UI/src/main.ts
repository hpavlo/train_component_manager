import { bootstrapApplication } from '@angular/platform-browser';
import { appConfig } from './app/app.config';
import { TrainComponentsComponent } from './app/train-components.component';

bootstrapApplication(TrainComponentsComponent, appConfig)
  .catch((err) => console.error(err));
