import { Component } from '@angular/core';
import { CategoryPanel } from '../category/category-panel/category-panel';
import { GiftPanel } from '../gift-panel/gift-panel';
import { DonorPanel } from '../donor/donor-panel/donor-panel';
import { PurchasePanel } from '../purchase/purchase-panel/purchase-panel';
import { RafflePanel } from '../raffle-panel/raffle-panel';

@Component({
  selector: 'app-admin-page',
  imports: [ RafflePanel],
  templateUrl: './admin-page.html',
  styleUrl: './admin-page.scss',
})
export class AdminPage {

}
