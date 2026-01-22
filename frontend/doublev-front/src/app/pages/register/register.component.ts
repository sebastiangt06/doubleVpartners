import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UsersService } from '../../services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html'
})
export class RegisterComponent {
  loading = false;
  error = '';
  success = '';

  form = this.fb.group({
    name: ['', [Validators.required]],
    lastName: ['', [Validators.required]],
    identificationType: ['CC', [Validators.required]],
    identification: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]],
    userName: ['', [Validators.required]],
    pass: ['', [Validators.required, Validators.minLength(6)]]
  });

  constructor(private fb: FormBuilder, private users: UsersService, private router: Router) {}

submit() {
  this.error = '';
  this.success = '';

  if (this.form.invalid) {
    this.form.markAllAsTouched();
    return;
  }

  this.loading = true;

  const payload = {
    name: this.form.value.name!,
    lastName: this.form.value.lastName!,
    identificationType: this.form.value.identificationType!,
    identification: this.form.value.identification!,
    email: this.form.value.email!,
    userName: this.form.value.userName!,
    pass: this.form.value.pass!
  };

  this.users.register(payload).subscribe({
    next: () => {
      this.loading = false;
      this.success = 'Usuario creado. Ahora puedes iniciar sesión.';
      setTimeout(() => this.router.navigate(['/login']), 700);
    },
    error: (err) => {
      this.loading = false;

      this.error = err?.error?.message ?? err?.error ?? 'No fue posible registrar.';
    }
  });
}

}
