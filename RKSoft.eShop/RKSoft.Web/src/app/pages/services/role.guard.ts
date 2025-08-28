import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';

@Injectable({ providedIn: 'root' })
export class RoleGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const token = localStorage.getItem('authToken');
    if (!token) {
      this.router.navigate(['/login']);
      return false;
    }

    const decoded: any = jwtDecode(token);
    const userRoles = Array.isArray(decoded['sub'])
      ? decoded['sub']
      : [decoded['sub']];

    const allowedRoles = route.data['roles'] as string[];
    if (allowedRoles.some(r => userRoles.includes(r))) {
      return true;
    }

    this.router.navigate(['/login']);
    return false;
  }
}
