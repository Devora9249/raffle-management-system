export interface Gift {
  id: number;
  description: string;
  price: number;
  categoryName: string;
  donorId: number;

  displayPrice: string;
}

export enum PriceSort {
  None = 'None',
  Ascending = 'Ascending',
  Descending = 'Descending',
}

