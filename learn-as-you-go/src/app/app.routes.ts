import { Routes } from '@angular/router';
import { homeRoutes } from '@features/home/home.routes';

export const routes: Routes = [
  {
    path: '',
    children: homeRoutes
  },
  {
    path: '**',
    redirectTo: ''
  }
];
