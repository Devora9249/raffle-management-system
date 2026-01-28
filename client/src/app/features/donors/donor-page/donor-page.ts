import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DonorService } from '../../../core/services/donor-service';
import { DonorListItem, DonorDashboardResponse } from '../../../core/models/donor-model';
import { DonorDashboard } from '../donor-dashboard/donor-dashboard';
import { DonatedGifts } from '../donated-gifts/donated-gifts';
import { DonorDetails } from '../donor-details/donor-details';
import { GiftResponseDto, GiftStatsDto } from '../../../core/models/gift-model';
import confetti from 'canvas-confetti';
@Component({
  selector: 'app-donor-page',
  imports: [CommonModule, DonorDashboard, DonatedGifts, DonorDetails],
  templateUrl: './donor-page.html',
  styleUrls: ['./donor-page.scss']
})
export class DonorPage implements OnInit {
  dashboard?: DonorDashboardResponse;
  details?: DonorListItem;
  gifts: GiftStatsDto[] = [];
  isLoading = true;

  constructor(private donorService: DonorService,) { }

  ngOnInit(): void {

    confetti({
      particleCount: 80,
      angle: 90,
      spread: 55,
      startVelocity: 45,
      gravity: 0.9,
      ticks: 200,
      scalar: 0.9,
      colors: ['#ff4d6d', '#ffffff', '#f69dad']
    });

    this.donorService.getMyDashboard().subscribe({
      next: dashboard => {
        this.dashboard = dashboard;
        this.gifts = dashboard.gifts;
        this.isLoading = false;
        console.log(dashboard, "dashboard");
      },
      error: err => {
        console.error(err);
        this.isLoading = false;
      }
    });

    this.donorService.getMyDetails().subscribe({
      next: details => {
        this.details = details;
        this.isLoading = false;
        console.log(details, "details");

      },
      error: err => {
        console.error(err);
        this.isLoading = false;
      }
    });
  }
}