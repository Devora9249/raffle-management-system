import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';

import { DonorService } from '../../../core/services/donor-service';
import { DonorDashboardResponse } from '../../../core/models/donor-model';

import { DonorDashboard } from '../donor-dashboard/donor-dashboard';
import { DonatedGifts } from '../donated-gifts/donated-gifts';
import { DonorDetails } from '../donor-details/donor-details';

@Component({
  selector: 'app-donor-page',
  standalone: true,
  imports: [
    CommonModule,
    DonorDashboard,
    DonatedGifts,
    DonorDetails
  ],
  templateUrl: './donor-page.html',
  styleUrl: './donor-page.scss'
})
export class DonorPage implements OnInit {

  donorDashboard?: DonorDashboardResponse;
  isLoading = true;

  constructor(
    private donorService: DonorService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const donorId = Number(this.route.snapshot.paramMap.get('donorId'));
console.log(donorId, "donorId in donor page");
    if (!donorId) {
      this.isLoading = false;
      return;
    }

    this.donorService.getDonorDashboard(donorId).subscribe({
      next: (data) => {
        this.donorDashboard = data;
        this.isLoading = false;
      },
      error: () => {
        this.isLoading = false;
      }
    });
  }
}
