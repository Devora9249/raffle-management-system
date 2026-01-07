import { Component } from '@angular/core';
import { DonorDashboard } from '../donor-dashboard/donor-dashboard';
import { DonatedGifts } from '../donated-gifts/donated-gifts';
import { DonorDetails } from '../donor-details/donor-details';


@Component({
  selector: 'app-donor-page',
  imports: [DonorDashboard, DonatedGifts, DonorDetails],
  templateUrl: './donor-page.html',
  styleUrl: './donor-page.scss',
})
export class DonorPage {

}
