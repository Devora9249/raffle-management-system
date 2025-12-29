import { Component } from '@angular/core';
import { GiftsService } from '../../../core/services/gifts-service';
import { Gift } from '../../../core/models/gift-model';

@Component({
  selector: 'app-gifts-page',
  imports: [],
  templateUrl: './gifts-page.html',
  styleUrl: './gifts-page.scss',
})
export class GiftsPage {
  gifts: Gift[] = []; // ⬅️ כאן הנתונים נשמרים

  constructor(private giftsService: GiftsService) {}

  ngOnInit(): void {
    this.giftsService.getAll().subscribe(gifts => {
      this.gifts = gifts;
    });
  }
}
