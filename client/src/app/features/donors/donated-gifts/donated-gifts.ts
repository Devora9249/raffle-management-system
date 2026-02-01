import { Component, Input } from '@angular/core';
import { DonorGiftStats } from '../../../core/models/donor-model';
import { GiftStatsDto } from '../../../core/models/gift-model';

@Component({
  selector: 'app-donated-gifts',
  imports: [],
  templateUrl: './donated-gifts.html',
  styleUrl: './donated-gifts.scss',
})
export class DonatedGifts {

   @Input() gifts!: GiftStatsDto[];

}
