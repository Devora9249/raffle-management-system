import { GiftResponseDto } from "./gift-model";

export interface DonorListItem {
  id: number;
  name: string;
  email: string;
  phone: string;
  city: string;
  address: string;
}

export interface DonorWithGiftsDto {
  id: number;
  name: string;
  email: string;
  phone: string;
  city: string;
  address: string;
  gifts: GiftResponseDto[];
}

export interface DonorDashboardResponse {
  donorId: number;
  donorName: string;
  totalGifts: number;
  totalTicketsSold: number;
  totalUniqueBuyers: number;

  gifts: DonorGiftStats[];
}

export interface DonorGiftStats {
  giftId: number;
  description: string;

  ticketsSold: number;
  uniqueBuyers: number;

  hasWinning: boolean;
}

export interface addDonorDto {
  name: string;
  email: string;
  password: string;
  phone: string;
  city: string;
  address: string;
}


export enum RoleEnum {
  User = 'User',
  Donor = 'Donor',
  Admin = 'Admin'
}


