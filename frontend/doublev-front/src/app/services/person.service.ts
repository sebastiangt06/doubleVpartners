import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../enviroments/enviroment';
import { map } from 'rxjs/operators';
import { unwrap } from './api-helpers';
import { Person } from '../models/Person';

@Injectable({ providedIn: 'root' })
export class PersonsService {
  constructor(private http: HttpClient) {}

  getCreated() {
    // Ajusta la ruta al endpoint real:
    // ejemplo: /api/persons/created
    return this.http.get<any>(`${environment.apiUrl}/persons/created`).pipe(
      map(res => unwrap<Person[]>(res))
    );
  }
}
