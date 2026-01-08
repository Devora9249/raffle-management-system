import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth-service';
import { LoginDto } from '../../../core/models/auth-model';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrls: ['./login.scss'],
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  submit() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    const dto: LoginDto = this.loginForm.value;

    this.authService.login(dto).subscribe({
      next: (res) => {
        this.authService.saveToken(res.token);
        alert('Login successful!');
        this.router.navigate(['/gifts']); // ניתוב אחרי התחברות
      },
      error: (err) => {
        console.error('Login failed', err);
        alert('Login failed. Check your credentials.');
      }
    });
  }
}
