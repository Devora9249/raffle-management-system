import { Component, Input } from '@angular/core';
import { DonorListItem } from '../../../core/models/donor-model';

@Component({
  selector: 'app-donor-details',
  imports: [],
  templateUrl: './donor-details.html',
  styleUrl: './donor-details.scss',
})
export class DonorDetails {
  @Input() donor!: DonorListItem ;
  @Input() donorId!: number;
@Input() donorName!: string;

}
