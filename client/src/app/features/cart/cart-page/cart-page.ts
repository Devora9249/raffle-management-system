import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { DrawerModule } from 'primeng/drawer';
import { CartDrawerService } from '../../../core/services/CartDrawerService ';
import { CartItemResponseDto } from '../../../core/models/cart-model';
import { CartService } from '../../../core/services/cart-service';
import { AuthService } from '../../../core/services/auth-service';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-cart-page',
  imports: [ButtonModule, DrawerModule, AsyncPipe],
  templateUrl: './cart-page.html',
  styleUrl: './cart-page.scss',
})
export class CartPage {

  visible: boolean = false;
  cartItems$!: Observable<CartItemResponseDto[]>;

  constructor(private cartDrawer: CartDrawerService, private cartService: CartService, private authService: AuthService) { }

  ngOnInit(): void {
    console.log("hi!");
    this.cartItems$ = this.cartService.cart$;
    this.cartDrawer.visible$.subscribe(v => this.visible = v);
    console.log(this.cartItems$, "cartitems");
  }
}