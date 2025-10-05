import { Component } from '@angular/core';
import { Routes } from '@angular/router';
import { Login } from './auth/login/login';
import { Layout } from './layout/layout/layout';
import { Dashboard } from './layout/dashboard/dashboard';
import { Home } from './pages/home/home';

export const routes: Routes = [
  {
      path:'',
      component: Login,
      pathMatch: 'full'
  },
  {
      path: 'login',
      component: Login
  },
  {
      path: '',
      component: Layout,
      children: [
        {
            path: 'dashboard',
            component: Dashboard
        },
        {
            path: 'home',
            component: Home
        }

      ]
  }
];
