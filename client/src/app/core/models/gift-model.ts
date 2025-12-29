// import { Injectable } from '@angular/core';

// @Injectable({
//   providedIn: 'root',
// })
// export class GiftModel {
  
// }
export interface Gift {
  id: number;
  description: string;
  price: number;
  categoryId: number;
  donorId: number;

  displayPrice: string;
}
