import { Component } from '@angular/core';
import { WinningService } from '../../../core/services/winning-service';
import { WinningResponseDto } from '../../../core/models/winning-model';

@Component({
  selector: 'app-raffle-panel',
  imports: [],
  templateUrl: './raffle-panel.html',
  styleUrl: './raffle-panel.scss',
})
export class RafflePanel {

  constructor(private winningService: WinningService) {}

  winnings: WinningResponseDto[] = []

startRaffle() {
  this.winningService.doRaffle().subscribe({
    next: (response: WinningResponseDto[]) => {
      this.winnings = response;
      console.log('Raffle started successfully:', response);
    },
    error: (error: any) => {
      console.error('Error starting raffle:', error);
    }
  });
}

}
