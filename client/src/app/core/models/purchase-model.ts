export interface PurchaseResponseDto {
  id: number;
  userId: number;
  userName: string;
  giftId: number;
  giftName: string;
  donorId: number;
  donorName: string;
  qty: number;
  status: Status;
  purchaseDate: string; 
}


export interface PurchaseCreateDto {
  userId: number;
  giftId: number;
  qty: number;
}

export interface PurchaseUpdateDto {
  qty?: number;
  status?: Status;
}

export interface GiftPurchaseCountDto {
  giftId: number;
  giftName: string;
  purchaseCount: number;
}

export enum Status {
  Pending = 'Pending',
  Approved = 'Approved',
  Canceled = 'Canceled'
}