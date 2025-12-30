import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { GiftCard } from './features/gifts/giftCard/gift-card/gift-card';
import { GiftsPage } from './features/gifts/gifts-page/gifts-page';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, GiftsPage, GiftCard],
  templateUrl: './app.html',
  styleUrl: './app.scss',
  standalone: true,
})
export class App {
  protected readonly title = signal('client');
}
