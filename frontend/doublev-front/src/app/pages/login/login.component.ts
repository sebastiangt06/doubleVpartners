import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent {
  loading = false;
  error = '';

  form = this.fb.group({
    userName: ['', [Validators.required]],
    password: ['', [Validators.required]]
  });

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) { }

  submit() {
    this.error = '';
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.loading = true;

    const payload = {
      username: this.form.value.userName!,
      password: this.form.value.password!
    };

    this.auth.login(payload).subscribe({
      next: () => {
        this.loading = false;
        this.router.navigate(['/persons']);
      },
      error: (err) => {
        this.loading = false;

        if (err?.status === 401) {
          this.error = 'Credenciales inválidas';
          return;
        }
        this.error = err?.error?.message ;
      }
    });
  }



}
