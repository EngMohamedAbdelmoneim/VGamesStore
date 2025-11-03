import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthResponse } from '../models/auth';
import { map, Observable } from 'rxjs';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

    private apiUrl = 'https://localhost:7229/api/Account'; // adjust port

  constructor(private http: HttpClient) {}

  private tokenKey = 'token';

  // local storage methods
  setToken(token: string) {
    localStorage.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
  }

  // service methods
  login(email: string, password: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/Login`, { email, password });
  }

  isLoggedIn(): boolean {
    const token = this.getToken();
    if (!token) return false;

    try {
      const decoded: any = jwtDecode(token);
      const exp = decoded.exp * 1000; // convert to ms

      return Date.now() < exp; // token still valid
    } catch {
      return false;
    }
  }

  register(email: string, password: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/Register`, { email, password });
  }

  refreshToken() {
    const refreshToken = localStorage.getItem('refreshToken');
    return this.http.post<AuthResponse>(`${this.apiUrl}/RefreshToken`, { refreshToken: refreshToken }).pipe(
      map(response => {
        this.setToken(response.token);
        localStorage.setItem('refreshToken', response.refreshToken);
        return response.token;
      })
    );
  }
  getRefreshToken() {
  return localStorage.getItem('refreshToken');
}
}
