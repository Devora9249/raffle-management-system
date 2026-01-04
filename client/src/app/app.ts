import { Component, signal } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { GiftsPage } from './features/gifts/gifts-page/gifts-page';
import { Register } from './features/auth/register/register';
import { ReactiveFormsModule } from '@angular/forms';
import { Login } from './features/auth/login/login';

@Component({
  selector: 'app-root',
  imports: [ RouterLink, RouterLinkActive, RouterOutlet, GiftsPage, Login, Register, ReactiveFormsModule],
  standalone: true,
  templateUrl: './app.html',
  styleUrl: './app.scss',
})

export class App {
  protected readonly title = signal('client');
}
