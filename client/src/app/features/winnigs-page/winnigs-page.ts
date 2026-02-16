import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WinningService } from '../../core/services/winning-service';
import { WinningResponseDto } from '../../core/models/winning-model';
import { CardModule } from 'primeng/card';
import { TagModule } from 'primeng/tag';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

@Component({
  selector: 'app-winnigs-page',
  standalone: true,
  imports: [CommonModule, CardModule, TagModule, ProgressSpinnerModule],
  templateUrl: './winnigs-page.html',
  styleUrl: './winnigs-page.scss',
})
export class WinnigsPage implements OnInit {
  winnings: WinningResponseDto[] = [];
  loading: boolean = false;

  constructor(private winningService: WinningService) {}

  ngOnInit() {
    this.loadWinnings();
  }

  loadWinnings() {
    this.loading = true;
    this.winningService.getAll().subscribe({
      next: (res) => {
        this.winnings = res;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error fetching winnings:', err);
        this.loading = false;
      }
    });
  }
}