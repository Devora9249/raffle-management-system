import { Component, EventEmitter, output, Output } from '@angular/core';
import { GiftsService } from '../../../core/services/gifts-service';
import { GiftCard } from '../giftCard/gift-card/gift-card';
import { GiftsGrid } from '../gifts-grid/gifts-grid';
import { GiftsHeader } from '../giftsHeader/gifts-header/gifts-header';
import { GiftResponseDto, PriceSort } from '../../../core/models/gift-model';
import { CategoriesService } from '../../../core/services/categories-service';
import { CategoryResponseDto } from '../../../core/models/category-model';

@Component({
  selector: 'app-gifts-page',
  imports: [GiftCard, GiftsGrid, GiftsHeader],
  templateUrl: './gifts-page.html',
  styleUrl: './gifts-page.scss',
  standalone: true,
})
export class GiftsPage {
  gifts: GiftResponseDto[] = [];
  categories: CategoryResponseDto[] = [];

  constructor(private giftsService: GiftsService, private categoriesService: CategoriesService) { }

  sortType: PriceSort = PriceSort.None;
  selectedCategoryId: number | null = null;






  ngOnInit(): void {
    this.giftsService.getAll(PriceSort.None).subscribe(gifts => {
      this.gifts = gifts;
    });

    this.categoriesService.getAll().subscribe(cats => {
    this.categories = cats;
  });
  console.log(this.categories, "categories");

  }

  loadAscending() {
    this.giftsService.getAll(PriceSort.Ascending)
      .subscribe(gifts => this.gifts = gifts);
  }

  loadDescending() {
    this.giftsService.getAll(PriceSort.Descending)
      .subscribe(gifts => this.gifts = gifts);
  }

  loadByCategory(categoryId: number | null) {
    this.giftsService.getGiftsByCategory(categoryId)
      .subscribe(gifts => this.gifts = gifts);
  }

  // @Output() sortChange = new EventEmitter<PriceSort>();

  onSortChange(sort: PriceSort) {
    console.log('sort change:', sort);
    if (sort === PriceSort.Ascending) {
      this.loadAscending();
    } else if (sort === PriceSort.Descending) {
      this.loadDescending();
    } else {
      this.ngOnInit();
    }
  }

  // @Output() categoryChange = new EventEmitter<number | null>();

  onCategoryChange(value: number | null): void {
    this.selectedCategoryId = value;
    if (value === null) {
      this.ngOnInit();
      return;
    }
    this.loadByCategory(value);
    console.log('selected category id in gifts page:', value);
  }

  onGiftCreated(): void {
    //רינדור אחרי הוספת מתנה
    this.ngOnInit();
  }

 
  onRender(): void {
    //רינדור אחרי מחיקה ועדכון מתנה
    this.ngOnInit();
  }

}
