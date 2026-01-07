import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import {CartAddDto,CartUpdateDto,CartItemResponseDto,CartCheckoutResponseDto} from '../models/cart-model';

@Injectable({ providedIn: 'root' })
export class CartService {

  private readonly baseUrl = 'http://localhost:5071/api/Cart';

  constructor(private http: HttpClient) {}

  // =========================
  // Get cart by user
  // GET /api/Cart/{userId}
  // =========================
  getCart(userId: number): Observable<CartItemResponseDto[]> {
    return this.http.get<CartItemResponseDto[]>(`${this.baseUrl}/${userId}`);
  }

  // =========================
  // Add item to cart (create draft)
  // POST /api/Cart
  // =========================
  add(dto: CartAddDto): Observable<CartItemResponseDto> {
    return this.http.post<CartItemResponseDto>(this.baseUrl, dto);
  }

  // =========================
  // Update quantity
  // PUT /api/Cart
  // =========================
  updateQty(dto: CartAddDto): Observable<CartItemResponseDto> {
    return this.http.put<CartItemResponseDto>(this.baseUrl, dto);
  }

  // =========================
  // Remove item from cart
  // DELETE /api/Cart/{purchaseId}
  // =========================
  remove(purchaseId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${purchaseId}`);
  }

  // =========================
  // Checkout cart
  // POST /api/Cart/checkout/{userId}
  // =========================
  checkout(userId: number): Observable<CartCheckoutResponseDto> {
    return this.http.post<CartCheckoutResponseDto>(
      `${this.baseUrl}/checkout/${userId}`,
      null
    );
  }
}
