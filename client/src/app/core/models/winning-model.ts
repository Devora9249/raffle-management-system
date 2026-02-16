export interface WinningResponseDto {
  id: number;
  giftId: number;
  giftName: string;
  winnerId: number;
  winnerName: string;
}

export interface WinningCreateDto {
  giftId: number;
  winnerId: number;
}