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
import { NotificationService } from '../../../core/services/notification-service';
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

  massage: string = '';

  constructor(
    private cartDrawer: CartDrawerService,
    private cartService: CartService,
    private authService: AuthService,
    private router: Router,
    private notificationService: NotificationService
  ) { }

  ngOnInit(): void {
    this.cartItems$ = this.cartService.cart$;
    this.totalPrice$ = this.cartService.totalPrice$;
    this.massage = '';


    this.cartDrawer.visible$.subscribe(visible => {
      this.visible = visible;

      if (visible) {
        this.authService.getCurrentUserId().pipe(take(1)).subscribe(userId => {
          if (userId === null) {
            this.massage = 'Please log in to view your cart.';
            return;
          }
          this.cartService.loadCart().subscribe();
        });
      }
    });
  }


  onCountChange(item: CartItemResponseDto, qty: number): void {
    if (item?.purchaseId === undefined) return;

    this.authService.getCurrentUserId().pipe(take(1)).subscribe(userId => {
      if (userId === null) {
        this.router.navigate(['/login']);
        return;
      }

      if (qty === 0) {
        this.cartService.remove(item.purchaseId).subscribe();
        return;
      }

      this.cartService.updateQty({
        giftId: item.giftId,
        qty
      }).subscribe();
    });
  }


  onDelete(item: CartItemResponseDto): void {
    if (item.purchaseId === undefined) return;
    this.notificationService.confirmDelete(() => {
      this.cartService.remove(item.purchaseId!).subscribe(() => {
        this.notificationService.showSuccess('deleted successfully ');
      });
    });
  }



  checkout(): void {
  this.authService.getCurrentUserId().pipe(take(1)).subscribe(userId => {
    if (userId === null) {
      this.router.navigate(['/login']);
      return;
    }

    this.cartItems$.pipe(take(1)).subscribe(items => {
      if (items.length === 0) {
        this.notificationService.showError('Your cart is empty');
        return;
      }

      this.cartDrawer.close();
      this.router.navigate(['/payment']); 
    });
  });
}
}
