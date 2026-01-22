import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../enviroments/enviroment';
import { tap, map } from 'rxjs/operators';
import { unwrap } from './api-helpers';
import { LoginRequest } from '../models/LoginRequest';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private tokenKey = 'doublev_token';

  constructor(private http: HttpClient) {}

  login(payload: LoginRequest) {
    return this.http.post<any>(`${environment.apiUrl}/login`, payload).pipe(
      map(res => unwrap<any>(res)),
      tap(res => {
        if (res?.token) localStorage.setItem(this.tokenKey, res.token);
      })
    );
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
  }

  get token(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  isLoggedIn(): boolean {
    return !!this.token;
  }
}

