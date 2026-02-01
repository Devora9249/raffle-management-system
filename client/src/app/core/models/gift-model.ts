
export interface GiftResponseDto {
  id: number;
  description: string;
  price: number;
  categoryName: string;
  categoryId: number;
  donorId: number;
  imageUrl: string;
}

export interface GiftCreateDto {
  description: string;
  price: number;
  categoryId: number;
  donorId: number;
  imageUrl?: string;
}

export interface GiftUpdateDto {
  description?: string;
  price?: number;
  categoryId?: number;
  imageUrl?: string;
}

export interface GiftStatsDto {
  giftId: number
  description: string,
  ticketsSold: number,
  uniqueBuyers: number,
  hasWinning: boolean
}

export enum PriceSort {
  None = 'None',
  Ascending = 'Ascending',
  Descending = 'Descending',
}



