import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-donor-dashboard',
  imports: [],
  templateUrl: './donor-dashboard.html',
  styleUrl: './donor-dashboard.scss',
})
export class DonorDashboard {
@Input() totalGifts!: number;
@Input() totalTicketsSold!: number;
@Input() totalUniqueBuyers!: number;
  
}
