export interface Gift {
  id: number;
  description: string;
  price: number;
  categoryId: number;
  donorId: number;

  displayPrice: string;
}

export enum PriceSort {
  None = 'None',
  Ascending = 'Ascending',
  Descending = 'Descending',
}

