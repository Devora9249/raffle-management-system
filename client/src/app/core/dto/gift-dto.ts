
export interface GiftResponseDto {
  id: number;
  description: string;
  price: number;
  categoryId: number;
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
