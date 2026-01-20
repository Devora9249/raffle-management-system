import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GiftCard } from '../giftCard/gift-card/gift-card';
import { GiftResponseDto } from '../../../core/models/gift-model';
import { CartItemResponseDto } from '../../../core/models/cart-model';

@Component({
  selector: 'app-gifts-grid',
  imports: [GiftCard],
  templateUrl: './gifts-grid.html',
  styleUrl: './gifts-grid.scss',
  standalone: true,
})
export class GiftsGrid {

  @Input() gifts: GiftResponseDto[] = [];
  @Input() isAdmin: boolean = false;
  @Input() cartItems: CartItemResponseDto[] = [];

  @Output() render = new EventEmitter<void>();
  @Output() edit = new EventEmitter<GiftResponseDto>();

  onEdit(gift: GiftResponseDto) {
    this.edit.emit(gift);
  }

  getPurchaseId(giftId: number): number | null {
    console.log("cartItems", this.cartItems);
    return this.cartItems.find(i => i.giftId === giftId)?.purchaseId ?? null;
  }

  getQty(giftId: number): number {
    return this.cartItems.find(i => i.giftId === giftId)?.qty ?? 0;
  }
}