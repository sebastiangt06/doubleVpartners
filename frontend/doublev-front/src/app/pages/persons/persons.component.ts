import { Component, OnInit } from '@angular/core';
import { PersonsService } from '../../services/person.service';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { Person } from '../../models/Person';

@Component({
  selector: 'app-persons',
  templateUrl: './persons.component.html'
})
export class PersonsComponent implements OnInit {
  loading = false;
  error = '';
  persons: Person[] = [];

  constructor(
    private personsService: PersonsService,
    private auth: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.loading = true;
    this.error = '';

    this.personsService.getCreated().subscribe({
      next: (res) => {
        this.loading = false;
        this.persons = res ?? [];
      },
      error: (err) => {
        this.loading = false;

        // Si expira token o no autorizado:
        if (err?.status === 401) {
          this.auth.logout();
          this.router.navigate(['/login']);
          return;
        }
        this.error = err?.error?.message ?? 'No fue posible cargar personas.';
      }
    });
  }
}
