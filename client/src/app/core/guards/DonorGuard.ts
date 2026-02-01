import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/auth-service';

@Injectable({ providedIn: 'root' })
export class DonorGuard implements CanActivate {

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  canActivate(): boolean {
        console.log('donorguerd!');

    if (this.authService.isLoggedIn() && this.authService.getRole() === 'Donor') {
      return true;
    }

    this.router.navigate(['/login']);
    return false;
  }
}
