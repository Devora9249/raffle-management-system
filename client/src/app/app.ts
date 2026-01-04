import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { GiftCard } from './features/gifts/giftCard/gift-card/gift-card';
import { GiftsPage } from './features/gifts/gifts-page/gifts-page';
import { Register } from './features/auth/register/register';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, GiftsPage, GiftCard, Register, ReactiveFormsModule],
  standalone: true,
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App {
  protected readonly title = signal('client');
}
