// Add item to cart (create draft purchase)
export interface CartAddDto {
  userId: number;
  giftId: number;
  qty: number;
}

// Update existing cart item quantity
export interface CartUpdateDto {
  purchaseId: number;
  qty: number;
}
// Cart item returned from server
export interface CartItemResponseDto {
  purchaseId: number;
  giftId: number;
  qty: number;
  addedAt: string; // ISO date string from server
}

// Checkout response
export interface CartCheckoutResponseDto {
  userId: number;
  itemsCompleted: number;
  message: string;
}
