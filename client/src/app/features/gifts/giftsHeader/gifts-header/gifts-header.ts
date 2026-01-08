import { Component, EventEmitter, Input, input, Output } from '@angular/core';
import { GiftFormDialog } from '../GiftFormDialog/gift-form-dialog';
import { SortPanel } from '../sort-panel/sort-panel';
import { GiftResponseDto, PriceSort } from '../../../../core/models/gift-model';
import { CategoryResponseDto } from '../../../../core/models/category-model';
import { FormsModule } from '@angular/forms';
import { DonorListItem } from '../../../../core/models/donor-model';

@Component({
  selector: 'app-gifts-header',
  imports: [SortPanel, FormsModule, GiftFormDialog],
  templateUrl: './gifts-header.html',
  styleUrl: './gifts-header.scss',
})
export class GiftsHeader {

  @Output() sortChange = new EventEmitter<PriceSort>();
  @Output() categoryChange = new EventEmitter<number | null>();
  @Output() created = new EventEmitter<boolean>();

  @Input() categories: CategoryResponseDto[] = [];
  @Input() donors: DonorListItem[] = [];
  @Input() sortType: PriceSort = PriceSort.None;
  @Input() selectedCategoryId: number | null = null;
  @Input() selectedGift: GiftResponseDto | null = null;

  onSortChange(sort: PriceSort) {
    this.sortType = sort;
    this.sortChange.emit(sort);
  }


  onCategoryChange(value: number | null): void {
    this.selectedCategoryId = value;
    this.categoryChange.emit(value);
  }

  onGiftChanged(): void {
    this.created.emit(true);
  }

}
