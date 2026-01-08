import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PriceSort } from '../../../../core/models/gift-model';
import { CategoryResponseDto } from '../../../../core/models/category-model';

@Component({
  selector: 'app-sort-panel',
  imports: [FormsModule],
  templateUrl: './sort-panel.html',
  styleUrl: './sort-panel.scss',
})
export class SortPanel implements OnInit {

  @Input() sortType: PriceSort = PriceSort.None;
  @Output() sortChange = new EventEmitter<PriceSort>();

  @Input() categories: CategoryResponseDto[] = [];
  @Output() categoryChange = new EventEmitter<number | null>();

  selectedCategoryId: number | null = null;

  PriceSort = PriceSort;

  ngOnInit(): void {
  }

  onSortChange(value: PriceSort): void {
    this.sortType = value;
    this.sortChange.emit(value);
  }

  onCategoryChange(value: number | null): void {
    this.selectedCategoryId = value;
    this.categoryChange.emit(value);
  }
}
