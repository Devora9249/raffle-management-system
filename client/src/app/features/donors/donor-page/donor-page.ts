import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DonorService } from '../../../core/services/donor-service';
import { AuthService } from '../../../core/services/auth-service';
import { DonorListItem, DonorDashboardResponse } from '../../../core/models/donor-model';
import { DonorDashboard } from '../donor-dashboard/donor-dashboard';
import { DonatedGifts } from '../donated-gifts/donated-gifts';
import { DonorDetails } from '../donor-details/donor-details';
import { switchMap } from 'rxjs/operators';
import { of } from 'rxjs'; // ✅ צריך להוסיף

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
  styleUrls: ['./donor-page.scss']
})
export class DonorPage implements OnInit {
  donorDashboard?: DonorDashboardResponse;
  donor?: DonorListItem;
  isLoading = true;

  constructor(
    private donorService: DonorService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
  this.donorService.getMyDonor().subscribe({
    next: donor => {
      this.donor = donor;
      this.isLoading = false;
    },
    error: err => {
      console.error(err);
      this.isLoading = false;
    }
  });
}
}