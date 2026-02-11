import { Component, Input, Output, EventEmitter } from '@angular/core';
import { GiftPurchaseCountDto } from '../../../../core/models/purchase-model';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-purchase-list',
  imports: [TableModule, ButtonModule],
  templateUrl: './purchase-list.html',
  styleUrl: './purchase-list.scss',
})
export class PurchaseList {

  @Input() purchases: GiftPurchaseCountDto[] = [];
  @Output() remove = new EventEmitter<number>();

}
