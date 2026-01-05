
export interface CategoryCreateDto {
  name: string;
}

export interface CategoryUpdateDto {
  name?: string;
}

export interface CategoryResponseDto {
  id: number;
  name: string;
}
