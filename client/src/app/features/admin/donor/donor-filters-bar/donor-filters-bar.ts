import { Component, EventEmitter, Input, output, Output } from '@angular/core';
import { DonorListItem } from '../../../../core/models/donor-model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-donor-filters-bar',
  imports: [FormsModule],
  templateUrl: './donor-filters-bar.html',
  styleUrl: './donor-filters-bar.scss',
})
export class DonorFiltersBar {

  @Input() searchTermGifts: string = '';
  @Input() searchTermDonors: string = '';

  @Output() searchTermGiftsChange = new EventEmitter<string>();
  @Output() searchTermDonorsChange = new EventEmitter<string>();

  @Output() filterChanged = new EventEmitter<void>();

  onGiftsChange(value: string): void {
    this.searchTermGiftsChange.emit(value);
    this.filterChanged.emit();
  }

  onDonorsChange(value: string): void {
    this.searchTermDonorsChange.emit(value);
    this.filterChanged.emit();
  }


}
