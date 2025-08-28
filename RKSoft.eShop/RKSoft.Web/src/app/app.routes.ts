import { Routes } from '@angular/router';
import { MainLayout } from './layout/main-layout/main-layout';
import { Dashboard } from './pages/dashboard/dashboard';
import { Login } from './pages/login/login';
import { Stores } from './pages/stores/stores';
import { RoleGuard } from './pages/services/role.guard';

export const routes: Routes = [
{ path: '', component: Login, pathMatch: 'full' },
  { path: 'login', component: Login },
  {
    path: '',
    component: MainLayout,
    children: [
      { path: 'dashboard', component: Dashboard },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'stores', component: Stores,canActivate: [RoleGuard],
  data: { roles: ['Admin'] } },
    ]
  },
  { path: '**', redirectTo: 'login' }
];
