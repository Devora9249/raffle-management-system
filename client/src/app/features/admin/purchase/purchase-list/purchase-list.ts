import { Component, Input } from '@angular/core';
import { GiftPurchaseCountDto } from '../../../../core/models/purchase-model';
import { TableModule } from 'primeng/table';

@Component({
  selector: 'app-purchase-list',
  imports: [TableModule],
  templateUrl: './purchase-list.html',
  styleUrl: './purchase-list.scss',
})
export class PurchaseList {

  @Input() purchases: GiftPurchaseCountDto[] = [];


}
