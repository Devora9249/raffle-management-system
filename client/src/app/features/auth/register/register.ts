import { Component, EventEmitter, Output } from '@angular/core';
import { ReactiveFormsModule, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { RegisterDto } from '../../../core/models/auth-model';
import { AuthService } from '../../../core/services/auth-service';
import { Router } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { PasswordModule } from 'primeng/password';
import { CommonModule } from '@angular/common';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule, ButtonModule, PasswordModule, CommonModule],
  templateUrl: './register.html',
  styleUrls: ['./register.scss'],
})
export class Register {

  form!: FormGroup;

  @Output() registered = new EventEmitter<RegisterDto>();

  constructor(
    private messageService: MessageService,
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.form = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      phone: [''],
      city: [''],
      address: ['']
    });
  }

  save() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const model: RegisterDto = this.form.value;

    this.authService.register(model).subscribe({
      next: () => {
            this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'Registration successful!',
          life: 3000
        });
        this.registered.emit(model);
        this.form.reset();
      },
      error: (err) => {
        let msg = 'Registration failed. Check your input.';
        if (err.status === 400 && err.error?.message) {
          msg = err.error.message;
        }
            this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: msg,
          life: 3000
        });
        console.error('Registration failed', err);
      }
    });
  }

  undoChanges() {
    this.form.reset();
  }
}
