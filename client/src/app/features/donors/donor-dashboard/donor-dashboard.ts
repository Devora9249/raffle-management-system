import { Component, input, Input } from '@angular/core';
import { DonorDashboardResponse } from '../../../core/models/donor-model';

@Component({
  selector: 'app-donor-dashboard',
  imports: [],
  templateUrl: './donor-dashboard.html',
  styleUrl: './donor-dashboard.scss',
})
export class DonorDashboard {
@Input() dashboard!: DonorDashboardResponse ;
  
}
