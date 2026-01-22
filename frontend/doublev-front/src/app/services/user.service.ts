import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../enviroments/enviroment';
import { map } from 'rxjs/operators';
import { unwrap } from './api-helpers';
import { RegisterRequest } from '../models/RegisterRequest';

@Injectable({ providedIn: 'root' })
export class UsersService {
  constructor(private http: HttpClient) {}

  register(payload: any) {
    return this.http.post<any>(`${environment.apiUrl}/Users/create`, payload).pipe(
      map(res => this.unwrap(res))
    );
  }

  private unwrap<T>(res: any): T {

    return (res?.data ?? res?.result ?? res) as T;
  }
}
