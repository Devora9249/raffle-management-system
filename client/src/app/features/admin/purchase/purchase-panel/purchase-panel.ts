import { Component } from '@angular/core';
import { GiftPurchaseCountDto, PurchaseResponseDto } from '../../../../core/models/purchase-model';
import { PurchaseService } from '../../../../core/services/purchase-service';
import { DatePipe } from '@angular/common';
import { PurchaseList } from '../purchase-list/purchase-list';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-purchase-panel',
  imports: [DatePipe, PurchaseList, FormsModule],
  templateUrl: './purchase-panel.html',
  styleUrl: './purchase-panel.scss',
})
export class PurchasePanel {
  allPurchases: GiftPurchaseCountDto[] = [];
  purchases: GiftPurchaseCountDto[] = [];
  searchTerm: string = '';

  constructor(private purchaseService: PurchaseService) {}

  ngOnInit(): void {
    this.purchaseService.getPurchaseCountByGift().subscribe({
      
      next: (purchases) => {
        console.log(purchases, 'purchases');

        this.purchases = purchases;
        this.allPurchases = purchases;
      },
      error: (error) => {
        console.error('Error loading purchases:', error);
      }
    });
  }

onSearchTermChange(): void {
  const term = this.searchTerm.toLowerCase();

  this.purchases = this.allPurchases.filter(p =>
    p.giftName.toLowerCase().includes(term)
  );
}

}
