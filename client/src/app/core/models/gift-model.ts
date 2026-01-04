
export interface GiftResponseDto {
  id: number;
  description: string;
  price: number;
  categoryName: string;
  donorId: number;
}

export interface GiftCreateDto {
  description: string;
  price: number;
  categoryId: number;
  donorId: number;
}

export interface GiftUpdateDto {
  description?: string;
  price?: number;
  categoryId?: number;
}

 export enum PriceSort {
  None = 'None',
  Ascending = 'Ascending',
  Descending = 'Descending',
}

