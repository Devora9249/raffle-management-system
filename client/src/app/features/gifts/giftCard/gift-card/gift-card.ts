import { Component, EventEmitter, Input, input, Output } from '@angular/core';
import { AddToCartButton } from '../add-to-cart-button/add-to-cart-button';
import { CartItemQty } from '../cart-item-qty/cart-item-qty';
import { AdminOptions } from '../admin-options/admin-options';
import { GiftResponseDto } from '../../../../core/models/gift-model';
import { CardModule } from 'primeng/card';
import { CartService } from '../../../../core/services/cart-service';
import { GiftsService } from '../../../../core/services/gifts-service';

@Component({
  selector: 'app-gift-card',
  imports: [AddToCartButton, CartItemQty, AdminOptions, CardModule],
  templateUrl: './gift-card.html',
  styleUrl: './gift-card.scss',
})
export class GiftCard {
  constructor(private cartService: CartService, private giftsService: GiftsService) { }

  @Input() gift!: GiftResponseDto;
  @Input() count!: number;

  @Output() render = new EventEmitter<boolean>();



  onCountChange(count: number) {
    this.count = count;
    this.cartService.updateQty({ userId: 18, giftId: this.gift.id, qty: this.count })
      .subscribe({
        next: cartItem => {
          console.log('Cart updated', cartItem);
        }
      });

  }

  onDelete() {

    this.giftsService.delete(this.gift.id).subscribe({
      next: gift => {
        console.log('Gift to delete:', gift);
        alert('Gift deleted successfully!');
        this.render.emit(true);
      },
      error: (err) => {
        console.error('Delete gift failed', err.error);
        alert('Delete gift failed,' + err.error.detail);
      }
    });
  }
  onEdit() {
    
  }
}
