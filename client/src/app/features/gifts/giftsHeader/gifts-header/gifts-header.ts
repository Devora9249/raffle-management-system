import { Component, EventEmitter, Input, input, Output } from '@angular/core';
import { AddProductButton } from '../add-product-button/add-product-button';
import { SortPanel } from '../sort-panel/sort-panel';
import { PriceSort } from '../../../../core/models/gift-model';
import { CategoryResponseDto } from '../../../../core/models/category-model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-gifts-header',
  imports: [AddProductButton, SortPanel, FormsModule],
  templateUrl: './gifts-header.html',
  styleUrl: './gifts-header.scss',
})
export class GiftsHeader {

  @Output() sortChange = new EventEmitter<PriceSort>();
  @Output() categoryChange = new EventEmitter<number | null>();
  @Output() created = new EventEmitter<boolean>();

  @Input() categories: CategoryResponseDto[] = [];
  @Input() sortType: PriceSort = PriceSort.None;
  @Input() selectedCategoryId: number | null = null;


  onSortChange(sort: PriceSort) {
    this.sortType = sort;
    this.sortChange.emit(sort);
  }


  onCategoryChange(value: number | null): void {
    this.selectedCategoryId = value;
    this.categoryChange.emit(value);
    console.log('selected category id:', value);
  }

  onGiftCreated(): void {
    this.created.emit(true);
  }

}
