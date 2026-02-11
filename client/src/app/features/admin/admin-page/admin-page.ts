import { Component } from '@angular/core';
import { Tabs, TabList, Tab, TabPanels, TabPanel } from 'primeng/tabs';
import { Card } from 'primeng/card';
import { PurchasePanel } from '../purchase/purchase-panel/purchase-panel';
import { DonorPanel } from '../donor/donor-panel/donor-panel';
import { CategoryPanel } from '../category/category-panel/category-panel';
import { RafflePanel } from '../raffle-panel/raffle-panel';

@Component({
  selector: 'app-admin-page',
  standalone: true,
  imports: [PurchasePanel, DonorPanel, CategoryPanel, RafflePanel, Tabs, TabList, Tab, TabPanels, TabPanel, Card],
  templateUrl: './admin-page.html',
  styleUrl: './admin-page.scss'
})
export class AdminPage {
  activeTab = 'raffle';
}
