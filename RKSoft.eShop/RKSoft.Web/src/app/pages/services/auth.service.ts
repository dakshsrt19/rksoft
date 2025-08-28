import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { jwtDecode } from 'jwt-decode';

interface JwtPayload {
  sub: string;
  role?: string | string[];
  exp: number;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginApiUrl = 'https://localhost:7039/api/Account';

  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<any> {
    return this.http.post(`${this.loginApiUrl}/login`, { username, password }).pipe(
      tap((res: any) => {
        if (res.token) {
          localStorage.setItem('authToken', res.token);
        }
      })
    );
  }
  getUserDetails(): any {
    const token = this.getToken();
    if (!token) return null;
    try {
      return jwtDecode<any>(token);
    } catch (error) {
      console.error('Invalid token', error);
      return null;
    }
  }
  logout() {
    localStorage.removeItem('authToken');
  }

  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  getUserRoles(): string[] {
    const token = this.getToken();
    if (!token) return [];
    const decoded = jwtDecode<JwtPayload>(token);
    if (Array.isArray(decoded.role)) return decoded.role;
    return decoded.role ? [decoded.role] : [];
  }

  isLoggedIn(): boolean {
    const token = this.getToken();
    if (!token) return false;
    const decoded = jwtDecode<JwtPayload>(token);
    return decoded.exp * 1000 > Date.now();
  }
}
