import { Component } from '@angular/core';
import { AddToCartButton } from '../add-to-cart-button/add-to-cart-button';
import { CartItemQty } from '../cart-item-qty/cart-item-qty';
import { AdminOptions } from '../admin-options/admin-options';


@Component({
  selector: 'app-gift-card',
  imports: [AddToCartButton, CartItemQty, AdminOptions],
  templateUrl: './gift-card.html',
  styleUrl: './gift-card.scss',
})
export class GiftCard {

}
