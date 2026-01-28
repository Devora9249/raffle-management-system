// WinningResponseDto – תואם ל־server.DTOs.WinningResponseDto
export interface WinningResponseDto {
  id: number;
  giftId: number;
  giftName: string;
  winnerId: number;
  winnerName: string;
}

// WinningCreateDto – תואם ל־server.DTOs.WinningCreateDto
export interface WinningCreateDto {
  giftId: number;
  winnerId: number;
}