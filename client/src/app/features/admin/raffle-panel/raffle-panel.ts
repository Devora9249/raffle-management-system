import { Component } from '@angular/core';
import { WinningService } from '../../../core/services/winning-service';
import { WinningResponseDto } from '../../../core/models/winning-model';
import { Spinner } from '../../../shared/components/spinner/spinner';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { PaginatorModule } from 'primeng/paginator';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

@Component({
  selector: 'app-raffle-panel',
  imports: [Spinner, CardModule, ButtonModule, PaginatorModule, TableModule, TagModule, ProgressSpinnerModule],
  templateUrl: './raffle-panel.html',
  styleUrl: './raffle-panel.scss',
  standalone: true
})
export class RafflePanel {

  winnings: WinningResponseDto[] = [];
  loading:boolean = false;

  constructor(private winningService: WinningService) {}

  ngOnInit() {
    this.loadWinnings();
  }

  loadWinnings() {
    this.loading = true;
    this.winningService.getAll().subscribe({
      next: res => {
        this.winnings = res;
        this.loading = false;
      },
      error: err => {
        console.error(err);
        this.loading = false;
      }
    });
  }

  startRaffle() {
    this.loading = true;

    this.winningService.doRaffle().subscribe({
      next: () => {
        this.loadWinnings(); // יסיים loading
      },
      error: err => {
        console.error(err);
        this.loading = false;
      }
    });
  }
}
