import { Component } from '@angular/core';
import { GiftPurchaseCountDto, PurchaseResponseDto } from '../../../../core/models/purchase-model';
import { PurchaseService } from '../../../../core/services/purchase-service';
import { DatePipe } from '@angular/common';
import { PurchaseList } from '../purchase-list/purchase-list';
import { FormsModule } from '@angular/forms';
import { NotificationService } from '../../../../core/services/notification-service';
import { GiftsService } from '../../../../core/services/gifts-service';

@Component({
  selector: 'app-purchase-panel',
  imports: [PurchaseList, FormsModule],
  templateUrl: './purchase-panel.html',
  styleUrl: './purchase-panel.scss',
})
export class PurchasePanel {
  allPurchases: GiftPurchaseCountDto[] = [];
  purchases: GiftPurchaseCountDto[] = [];
  searchTerm: string = '';


  constructor(private giftsService: GiftsService, private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.loadPurchases();
  }

  loadPurchases(): void {
    this.giftsService.getPurchaseCountByGift().subscribe({

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

  delete(id: number): void {
    this.notificationService.confirmDelete(() => {
      this.giftsService.delete(id).subscribe({
        next: () => {
          this.loadPurchases();
          this.notificationService.showSuccess('Gift deleted successfully');
        },
        error: (err) => {
          console.error('Delete gift failed', err);
          const message =
            err?.error?.detail ||
            err?.error?.message ||
            'Unauthorized or unexpected error';

          this.notificationService.showError('Delete gift failed: ' + message);
        }
      });
    });
  }

}
