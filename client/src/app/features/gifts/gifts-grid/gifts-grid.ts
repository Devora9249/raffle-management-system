import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GiftCard } from '../giftCard/gift-card/gift-card';
import { GiftResponseDto } from '../../../core/models/gift-model';

@Component({
  selector: 'app-gifts-grid',
  imports: [GiftCard],
  templateUrl: './gifts-grid.html',
  styleUrl: './gifts-grid.scss',
  standalone: true,
})
export class GiftsGrid {
  
  @Input() gifts: GiftResponseDto[] = [];
  @Output() render = new EventEmitter<void>();
  @Output() edit = new EventEmitter<GiftResponseDto>();

  onEdit(gift: GiftResponseDto) {
    console.log("22222222222");
    this.edit.emit(gift);
  }
}
