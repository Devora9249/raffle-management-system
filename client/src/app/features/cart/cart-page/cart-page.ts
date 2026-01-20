import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { DrawerModule } from 'primeng/drawer';
import { CartDrawerService } from '../../../core/services/CartDrawerService ';
import { CartItemResponseDto } from '../../../core/models/cart-model';
import { CartService } from '../../../core/services/cart-service';
import { AuthService } from '../../../core/services/auth-service';

@Component({
  selector: 'app-cart-page',
  imports: [ButtonModule, DrawerModule],
  templateUrl: './cart-page.html',
  styleUrl: './cart-page.scss',
})
export class CartPage {

  visible: boolean = false;
  // cartItems: CartItemResponseDto[] = [];

  constructor(private cartDrawer: CartDrawerService, private cartService: CartService, private authService: AuthService) { }

  ngOnInit(): void {
    this.cartDrawer.visible$.subscribe(v => this.visible = v);

    // this.authService.getCurrentUserId().subscribe(userId => {
    //   if (!userId) return;
    //   this.cartService.getCart(userId).subscribe(items => {
    //     this.cartItems = items;
    //   });
    // });
  }
}