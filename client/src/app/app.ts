import { Component, signal, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { CommonModule, NgIf } from '@angular/common'; // <-- הוספנו
import { GiftsPage } from './features/gifts/gifts-page/gifts-page';
import { Register } from './features/auth/register/register';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './features/auth/login/login';
import { AuthService } from './core/services/auth-service';
import { UserResponseDto } from './core/models/auth-model';
import { Nav } from './shared/components/nav/nav';
import { CartPage } from './features/cart/cart-page/cart-page';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
@Component({
  selector: 'app-root',
  imports: [
    CommonModule, 
 RouterOutlet,
   ReactiveFormsModule, Nav,
    CartPage,
    ToastModule,
    ConfirmDialogModule
  ],
  templateUrl: './app.html',
  styleUrls: ['./app.scss'],
})
export class App implements OnInit {
  protected readonly title = signal('client');
  currentDonorId?: number;
  // isDonor: boolean = false;

  constructor(private authService: AuthService) { }

  ngOnInit() {
    this.authService.getCurrentUser().subscribe((user) => {
      if (!user) return; 
      console.log(user.name);
      this.currentDonorId = user.id;
    });
  }

  // get isDonor(): boolean {
  //   return this.authService.isDonor();
  // }
}
