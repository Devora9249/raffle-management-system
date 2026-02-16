import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../core/services/auth-service';
import { LoginDto } from '../../../core/models/auth-model';
import { ButtonModule } from 'primeng/button';
import { PasswordModule } from 'primeng/password';
import { InputGroupModule } from 'primeng/inputgroup';
import { CommonModule } from '@angular/common';
import { MessageService } from 'primeng/api';
import { NotificationService } from '../../../core/services/notification-service';
import { ValidationErrorDirective } from '../../../shared/directives/validation-error';
@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, ButtonModule, PasswordModule, InputGroupModule, CommonModule, ValidationErrorDirective],
  templateUrl: './login.html',
  styleUrls: ['./login.scss'],
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(
private notification: NotificationService,
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
        this.notification.showSuccess('Login successful!');
        this.router.navigate(['/gifts']);
      }
    });
  
  }
  
}
