import { Component, OnInit } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { DrawerModule } from 'primeng/drawer';
import { AsyncPipe } from '@angular/common';
import { Router } from '@angular/router';
import { Observable, take } from 'rxjs';

import { CartDrawerService } from '../../../core/services/CartDrawerService ';
import { CartService } from '../../../core/services/cart-service';
import { AuthService } from '../../../core/services/auth-service';
import { CartItemResponseDto } from '../../../core/models/cart-model';
import { CartItemQty } from '../../gifts/giftCard/cart-item-qty/cart-item-qty';

@Component({
  selector: 'app-cart-page',
  standalone: true,
  imports: [ButtonModule, DrawerModule, AsyncPipe, CartItemQty],
  templateUrl: './cart-page.html',
  styleUrl: './cart-page.scss',
})
export class CartPage implements OnInit {

  visible = false;

  cartItems$!: Observable<CartItemResponseDto[]>;

  totalPrice$!: Observable<number>;

  constructor(
    private cartDrawer: CartDrawerService,
    private cartService: CartService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.cartItems$ = this.cartService.cart$;
    this.totalPrice$ = this.cartService.totalPrice$;

    this.cartDrawer.visible$.subscribe(visible => {
      this.visible = visible;

      if (visible) {
        this.authService.getCurrentUserId().pipe(take(1)).subscribe(userId => {
          if (!userId) return;
          this.cartService.loadCart(userId).subscribe(); 
        });
      }
    });
  }


  onCountChange(item: CartItemResponseDto, qty: number): void {
    if (!item?.purchaseId) return;

    this.authService.getCurrentUserId().pipe(take(1)).subscribe(userId => {
      if (!userId) {
        this.router.navigate(['/login']); 
        return;
      }

      if (qty === 0) {
        this.cartService.remove(item.purchaseId).subscribe();
        return;
      }

      this.cartService.updateQty({
        userId,
        giftId: item.giftId,
        qty
      }).subscribe();
    });
  }


  onDelete(item: CartItemResponseDto): void {
    if (!item.purchaseId) return;

    this.cartService.remove(item.purchaseId).subscribe();
  }


  checkout(): void {
    //אימות משתמש
    this.authService.getCurrentUserId().pipe(take(1)).subscribe(userId => {
      if (!userId) {
        this.router.navigate(['/login']);
        return;
      }

      //בדיקת סל
      this.cartItems$.pipe(take(1)).subscribe(items => {
        if (items.length === 0) {
          alert('Your cart is empty. Please add items before checkout.');
          return;
        }

        //checkout
        this.cartService.checkout(userId).subscribe({
          next: () => {
            alert('Checkout successful!');
          },
          error: err => {
            console.error('Checkout failed', err);
            alert('Checkout failed. Please try again later.');
          }
        });
      });
    });
  }
}
