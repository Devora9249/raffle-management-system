// ==============================
// Cart Models (Client Side DTOs)
// ==============================

// -----------
// Add item to cart (create draft)
// -----------
export interface CartAddDto {
  userId: number;
  giftId: number;
  qty: number;
}

// -----------
// Update quantity of existing cart item
// -----------
export interface CartUpdateDto {
  purchaseId: number;
  qty: number;
}

// -----------
// Cart item returned from server
// -----------
export interface CartItemResponseDto {
  purchaseId: number;
  giftId: number;
  giftName: string;
  giftPrice: number;
  qty: number;
  addedAt: string; 
}

// -----------
// Checkout response
// -----------
export interface CartCheckoutResponseDto {
  userId: number;
  itemsCompleted: number;
  message: string;
}
