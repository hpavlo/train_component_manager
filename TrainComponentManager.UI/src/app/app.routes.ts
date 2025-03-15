import { Routes } from '@angular/router';
import { TrainComponentsComponent } from './train-components.component';

export const routes: Routes = [
    { path: '', redirectTo: '/components', pathMatch: 'full' },
    { path: 'components', component: TrainComponentsComponent }
];
