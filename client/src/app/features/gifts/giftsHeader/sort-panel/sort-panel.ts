import { Component, EventEmitter, Input, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PriceSort } from '../../../../core/models/gift-model';
import { CategoryResponseDto } from '../../../../core/models/category-model';
import { SelectModule } from 'primeng/select';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-sort-panel',
  imports: [FormsModule, SelectModule, ButtonModule],
  templateUrl: './sort-panel.html',
  styleUrl: './sort-panel.scss',
})
export class SortPanel implements OnInit {

  @Input() sortType: PriceSort = PriceSort.None;
  @Output() sortChange = new EventEmitter<PriceSort>();

  @Input() categories: CategoryResponseDto[] = [];
  @Output() categoryChange = new EventEmitter<number | null>();

  priceSortOptions = [
    { label: 'no sort', value: PriceSort.None },
    { label: 'From low to high', value: PriceSort.Ascending },
    { label: 'From high to low', value: PriceSort.Descending }
  ];

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


  categoryOptions: { label: string; value: number | null }[] = [];

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['categories']) {
      this.categoryOptions = [
        { label: 'All Categories', value: null },
        ...this.categories.map(c => ({
          label: c.name,
          value: c.id
        }))
      ];
    }
  }
}
