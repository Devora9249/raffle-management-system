import { Component, EventEmitter, output, Output } from '@angular/core';
import { GiftsService } from '../../../core/services/gifts-service';
import { GiftCard } from '../giftCard/gift-card/gift-card';
import { GiftsGrid } from '../gifts-grid/gifts-grid';
import { GiftsHeader } from '../giftsHeader/gifts-header/gifts-header';
import { GiftResponseDto, PriceSort } from '../../../core/models/gift-model';
import { CategoriesService } from '../../../core/services/categories-service';
import { CategoryResponseDto } from '../../../core/models/category-model';
import { DonorListItem } from '../../../core/models/donor-model';
import { DonorService } from '../../../core/services/donor-service';

@Component({
  selector: 'app-gifts-page',
  imports: [GiftCard, GiftsGrid, GiftsHeader],
  templateUrl: './gifts-page.html',
  styleUrl: './gifts-page.scss',
})
export class GiftsPage {

  gifts: GiftResponseDto[] = [];
  categories: CategoryResponseDto[] = [];
  donors: DonorListItem[] = [];
  selectedGift: GiftResponseDto | null = null;

  constructor(private giftsService: GiftsService, private categoriesService: CategoriesService, private donorService: DonorService) { }

  sortType: PriceSort = PriceSort.None;
  selectedCategoryId: number | null = null;



  ngOnInit(): void {
    this.giftsService.getAll(PriceSort.None, null, null).subscribe(gifts => {
      this.gifts = gifts;
    });

    this.categoriesService.getAll().subscribe(cats => {
      this.categories = cats;
    });

    this.donorService.getAll().subscribe(donors => {
      this.donors = donors;
    });

  }

  loadGifts(): void {
    this.giftsService.getAll(this.sortType, this.selectedCategoryId, null).subscribe(gifts => {
      this.gifts = gifts;
    });
  }


  onSortChange(sort: PriceSort) {
    this.sortType = sort;
    this.loadGifts();
  }


  onCategoryChange(value: number | null): void {
    this.selectedCategoryId = value;
    this.loadGifts();
  }

  onGiftCreated(): void {
    //רינדור אחרי הוספת מתנה
    this.ngOnInit();
  }


  onRender(): void {
    //רינדור אחרי מחיקה ועדכון מתנה
    this.ngOnInit();
  }

  onEdit(gift: GiftResponseDto): void {
    this.selectedGift = {...gift};
  }

  onDialogClosed(): void {
    this.selectedGift = null;
  }

}
