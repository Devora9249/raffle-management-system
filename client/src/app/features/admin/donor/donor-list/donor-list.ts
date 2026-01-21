import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DonorListItem } from '../../../../core/models/donor-model';
import { TableModule } from 'primeng/table';

@Component({
  selector: 'app-donor-list',
  imports: [TableModule],
  templateUrl: './donor-list.html',
  styleUrl: './donor-list.scss',
})
export class DonorList {
  @Input() donors: DonorListItem[] = [];
  // @Output() edit = new EventEmitter<DonorListItem>();
  // @Output() remove = new EventEmitter<number>();
}
