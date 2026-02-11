import { Component, Input, Output, EventEmitter } from '@angular/core';
import { GiftPurchaseCountDto } from '../../../../core/models/purchase-model';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { TagModule } from 'primeng/tag';

@Component({
  selector: 'app-purchase-list',
  imports: [TableModule, ButtonModule, TagModule],
  templateUrl: './purchase-list.html',
  styleUrl: '../../globalAdminDesign.scss',
})
export class PurchaseList {

  @Input() purchases: GiftPurchaseCountDto[] = [];
  @Output() remove = new EventEmitter<number>();

}
