import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { LoginResponse } from '../models/login-response';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl = 'http://localhost:5122/api/Auth';

  constructor(private http: HttpClient) {}

  findTenants(email: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/FindTenants`, {params: { email }});
  }

  login(email: string, password: string, tenantId: number): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.baseUrl}/Login`, { email, password, tenantId }).pipe(
      tap(res => {
        localStorage.setItem('token', res.token);
        localStorage.setItem('refreshToken', res.refreshToken);
        localStorage.setItem('role', res.role);
        localStorage.setItem('userId', res.userId.toString());
        localStorage.setItem('tenantId', res.tenantId.toString());
      })
    );
  }

  logout() {
    localStorage.clear();
  }

  refreshToken() {
    const refreshToken = localStorage.getItem('refreshToken');
    if (!refreshToken) return throwError(() => new Error('No refresh token'));

    return this.http.post<LoginResponse>(`${this.baseUrl}/Refresh`, { refreshToken })
      .pipe(
        tap(res => {
          localStorage.setItem('token', res.token);
          localStorage.setItem('refreshToken', res.refreshToken);
          localStorage.setItem('role', res.role);
          localStorage.setItem('userId', res.userId.toString());
          localStorage.setItem('tenantId', res.tenantId.toString());
        })
      );
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getRole(): string | null {
    return localStorage.getItem('role');
  }

  getUserId(): number {
    return Number(localStorage.getItem('userId')) || 0;
  }

  getTenantId(): number {
    return Number(localStorage.getItem('tenantId')) || 0;
  }
}
