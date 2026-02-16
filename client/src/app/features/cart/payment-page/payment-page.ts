import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { toSignal } from '@angular/core/rxjs-interop';
import { take } from 'rxjs';

// Services
import { CartService } from '../../../core/services/cart-service';
import { AuthService } from '../../../core/services/auth-service';
import { NotificationService } from '../../../core/services/notification-service';
import { ValidationErrorDirective } from '../../../shared/directives/validation-error';

// PrimeNG
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { CardModule } from 'primeng/card';

@Component({
  selector: 'app-payment-page',
  standalone: true,
  imports: [
    CommonModule, 
    ReactiveFormsModule, 
    ButtonModule, 
    InputTextModule, 
    CardModule, 
    ValidationErrorDirective 
  ],
  templateUrl: './payment-page.html',
  styleUrl: './payment-page.scss'
})
export class PaymentPage implements OnInit {
  // שימוש ב-inject פותר את שגיאת ה-Initialization
  private cartService = inject(CartService);
  private authService = inject(AuthService);
  private router = inject(Router);
  private fb = inject(FormBuilder);
  private notificationService = inject(NotificationService);

  // המרת ה-Observable ל-Signal 
  totalPrice = toSignal(this.cartService.totalPrice$, { initialValue: 0 });

  paymentForm!: FormGroup;
  loading: boolean = false;

  constructor() {}

  ngOnInit(): void {
    this.paymentForm = this.fb.group({
      cardNumber: ['', [Validators.required, Validators.pattern('^[0-9]{16}$')]],
      expiryDate: ['', [Validators.required, Validators.pattern('^(0[1-9]|1[0-2])\/([0-9]{2})$')]],
      cvv: ['', [Validators.required, Validators.pattern('^[0-9]{3}$')]],
      idNumber: ['', [Validators.required, Validators.pattern('^[0-9]{9}$')]]
    });
  }

  processPayment() {
    if (this.paymentForm.invalid) return;

    this.loading = true;
    this.authService.getCurrentUserId().pipe(take(1)).subscribe(userId => {
      if (!userId) {
        this.notificationService.showError('User not connected');
        this.loading = false;
        return;
      }

      this.cartService.checkout(userId).subscribe({
        next: () => {
          this.loading = false;
          this.notificationService.showSuccess('Payment successful! Your order is on its way.');
          
          // חזרה לדף הבית אחרי הצלחה
          setTimeout(() => this.router.navigate(['/']), 2000);
        },
        error: (err) => {
          this.loading = false;
          this.notificationService.showError('Payment failed. Please check your details.');
          console.error(err);
        }
      });
    });
  }

  goBack() {
    this.router.navigate(['/']);
  }
}