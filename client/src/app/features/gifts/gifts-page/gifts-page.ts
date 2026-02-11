import { Component, EventEmitter, output, Output, ViewChild } from '@angular/core';
import { GiftsService } from '../../../core/services/gifts-service';
import { GiftCard } from '../giftCard/gift-card/gift-card';
import { GiftsGrid } from '../gifts-grid/gifts-grid';
import { GiftsHeader } from '../giftsHeader/gifts-header/gifts-header';
import { GiftResponseDto, PriceSort } from '../../../core/models/gift-model';
import { CategoriesService } from '../../../core/services/categories-service';
import { CategoryResponseDto } from '../../../core/models/category-model';
import { DonorListItem } from '../../../core/models/donor-model';
import { DonorService } from '../../../core/services/donor-service';
import { GiftFormDialog } from '../giftsHeader/GiftFormDialog/gift-form-dialog';
import { AuthService } from '../../../core/services/auth-service';
import { CartItemResponseDto } from '../../../core/models/cart-model';
import { CartService } from '../../../core/services/cart-service';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';
import { NotificationService } from '../../../core/services/notification-service';
@Component({
  selector: 'app-gifts-page',
  imports: [ GiftsGrid, GiftsHeader, GiftFormDialog, AsyncPipe],
  templateUrl: './gifts-page.html',
  styleUrl: './gifts-page.scss',
})
export class GiftsPage {

  gifts: GiftResponseDto[] = [];
  categories: CategoryResponseDto[] = [];
  donors: DonorListItem[] = [];
  selectedGift: GiftResponseDto | null = null;
  isAdmin: boolean = false;
  // cartItems: CartItemResponseDto[] = [];
  userId: number | null = null;
  cartItems$!: Observable<CartItemResponseDto[]>;
  constructor(private giftsService: GiftsService, private categoriesService: CategoriesService, private donorService: DonorService, private authService: AuthService, private cartService: CartService,private notificationService: NotificationService) { }

  sortType: PriceSort = PriceSort.None;
  selectedCategoryId: number | null = null;

  @ViewChild(GiftFormDialog) giftDialog!: GiftFormDialog;



  ngOnInit(): void {
    this.cartItems$ = this.cartService.cart$; // ✅ כאן זה כבר בטוח

    this.giftsService.getAll(PriceSort.None, null, null).subscribe(gifts => {
      this.gifts = gifts;
    });

    this.categoriesService.getAll().subscribe(cats => {
      this.categories = cats;
    });

    this.donorService.getDonors().subscribe(donors => {
      this.donors = donors;
    });

    this.authService.isAdmin$.subscribe(isAdmin => {
      this.isAdmin = isAdmin;
    });

    this.authService.getCurrentUserId().subscribe(userId => {
      if (!userId) return;
      this.userId = userId;
      this.cartService.loadCart(this.userId).subscribe();
    });

  }



  // getPurchaseId(giftId: number): number | null {
  //   return this.cartItems.find(i => i.giftId === giftId)?.purchaseId ?? null;
  // }

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
    this.loadGifts();
  }


  onRender(): void {
    //רינדור אחרי מחיקה ועדכון מתנה
    this.loadGifts();
  }

  onEdit(gift: GiftResponseDto): void {
    this.selectedGift = { ...gift };
  }

  onDialogClosed(): void {
    this.selectedGift = null;
  }

  onClickAddGift(): void {
    this.selectedGift = null;
    this.giftDialog.open();
    console.log("clicked!");
  }
}
