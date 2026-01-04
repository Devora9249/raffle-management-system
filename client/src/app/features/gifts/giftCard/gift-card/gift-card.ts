import { Component, Input, input } from '@angular/core';
import { AddToCartButton } from '../add-to-cart-button/add-to-cart-button';
import { CartItemQty } from '../cart-item-qty/cart-item-qty';
import { AdminOptions } from '../admin-options/admin-options';
import { GiftResponseDto } from '../../../../core/models/gift-model';
import { CardModule } from 'primeng/card';


@Component({
  selector: 'app-gift-card',
  imports: [AddToCartButton, CartItemQty, AdminOptions, CardModule],
  templateUrl: './gift-card.html',
  styleUrl: './gift-card.scss',
})
export class GiftCard {
   @Input() gift!: GiftResponseDto ;

}
