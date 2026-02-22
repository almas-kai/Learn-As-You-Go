import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadChildren: () => import('@home-feature/home.routes')
      .then(r => r.HOME_ROUTES)
  },
  {
    path: '**',
    redirectTo: ''
  }
];
