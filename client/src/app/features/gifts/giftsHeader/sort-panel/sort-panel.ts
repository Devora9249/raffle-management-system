import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PriceSort } from '../../../../core/models/gift-model';

@Component({
  selector: 'app-sort-panel',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './sort-panel.html',
  styleUrl: './sort-panel.scss',
})
export class SortPanel implements OnInit {

  @Input() sortType: PriceSort = PriceSort.None;
  @Output() sortChange = new EventEmitter<PriceSort>();


  PriceSort = PriceSort;

  ngOnInit(): void {
    // console.log('init:', this.sortType);
  }

  onSortChange(value: PriceSort): void {
    this.sortType = value;
    this.sortChange.emit(value);

  }
}
