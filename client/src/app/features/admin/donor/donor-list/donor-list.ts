import { Component, EventEmitter, Input, output, Output } from '@angular/core';
import { DonorListItem, DonorWithGiftsDto } from '../../../../core/models/donor-model';
import { TableModule } from 'primeng/table';

@Component({
  selector: 'app-donor-list',
  imports: [TableModule],
  templateUrl: './donor-list.html',
  styleUrl: '../../globalAdminDesign.scss',
})
export class DonorList {
  @Input() donors: DonorWithGiftsDto[] = [];
  @Output() selectedDonor = new EventEmitter<DonorWithGiftsDto>();


}
