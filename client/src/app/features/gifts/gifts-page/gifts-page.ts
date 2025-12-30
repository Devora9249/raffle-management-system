import { Component } from '@angular/core';
import { GiftsService } from '../../../core/services/gifts-service';
import { Gift } from '../../../core/models/gift-model';
import { GiftCard } from '../giftCard/gift-card/gift-card';
import { GiftsGrid } from '../gifts-grid/gifts-grid';
import { GiftsHeader } from '../giftsHeader/gifts-header/gifts-header';
import { PriceSort } from '../../../core/models/gift-model';

@Component({
  selector: 'app-gifts-page',
  imports: [GiftCard, GiftsGrid, GiftsHeader],
  templateUrl: './gifts-page.html',
  styleUrl: './gifts-page.scss',
  standalone: true,
})
export class GiftsPage {
  gifts: Gift[] = []; 

  constructor(private giftsService: GiftsService) {}

  ngOnInit(): void {
    this.giftsService.getAll().subscribe(gifts => {
      this.gifts = gifts;
      console.log(gifts, 'gifts in gp');
      
    });
  }

  loadAscending() {
  this.giftsService.getAll(PriceSort.Ascending)
    .subscribe(gifts => this.gifts = gifts);
}

loadDescending() {
  this.giftsService.getAll(PriceSort.Descending)
    .subscribe(gifts => this.gifts = gifts);
}
}
