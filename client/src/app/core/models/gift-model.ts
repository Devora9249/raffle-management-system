export interface Gift {
  id: number;
  description: string;
  price: number;
  categoryId: number;
  donorId: number;

  displayPrice: string;
}
