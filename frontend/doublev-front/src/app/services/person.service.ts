import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../enviroments/enviroment';
import { map } from 'rxjs/operators';
import { unwrap } from './api-helpers';
import { Person } from '../models/Person';
import { RegisterRequest } from '../models/RegisterRequest';

@Injectable({ providedIn: 'root' })
export class PersonsService {
  constructor(private http: HttpClient) { }

  getCreated() {

    return this.http.get<any>(`${environment.apiUrl}/Persons/get`).pipe(
      map(res => unwrap<any>(res))
    );
  }
}
