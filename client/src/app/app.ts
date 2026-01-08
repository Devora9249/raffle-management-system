import { Component, signal, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { CommonModule, NgIf } from '@angular/common'; // <-- הוספנו
import { GiftsPage } from './features/gifts/gifts-page/gifts-page';
import { Register } from './features/auth/register/register';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './features/auth/login/login';
import { AuthService } from './core/services/auth-service';
import { UserResponseDto } from './core/models/auth-model';

@Component({
  selector: 'app-root',
  imports: [
    CommonModule, // <-- חובה ל-*ngIf
    NgIf,         // <-- חובה ל-*ngIf
    RouterLink, RouterLinkActive, RouterOutlet,
    GiftsPage, LoginComponent, Register, ReactiveFormsModule
  ],
  templateUrl: './app.html',
  styleUrls: ['./app.scss'],
})
export class App implements OnInit {
  protected readonly title = signal('client');
  currentDonorId?: number;

  constructor(private authService: AuthService) {}

  ngOnInit() {
this.authService.getCurrentUser().subscribe((user) => {
  if (!user) return; // ✅ בדיקה אם null
  console.log(user.name); // עכשיו בטוח שזה UserResponseDto
  this.currentDonorId = user.id;
});

  }
}
