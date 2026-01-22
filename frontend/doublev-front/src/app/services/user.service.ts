import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../enviroments/enviroment';
import { map } from 'rxjs/operators';
import { unwrap } from './api-helpers';
import { RegisterRequest } from '../models/RegisterRequest';

@Injectable({ providedIn: 'root' })
export class UsersService {
  constructor(private http: HttpClient) {}

  register(payload: RegisterRequest) {
    // Ajusta la ruta al endpoint real que creaste:
    // ejemplo: /api/users/register  o /api/users/create
    return this.http.post<any>(`${environment.apiUrl}/users/register`, payload).pipe(
      map(res => unwrap<any>(res))
    );
  }
}
