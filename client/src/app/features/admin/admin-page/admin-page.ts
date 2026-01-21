import { Component } from '@angular/core';
import { CategoryPanel } from '../category-panel/category-panel';
import { DonorsPanel } from '../donors-panel/donors-panel';

@Component({
  selector: 'app-admin-page',
  imports: [CategoryPanel, DonorsPanel, gifts],
  templateUrl: './admin-page.html',
  styleUrl: './admin-page.scss',
})
export class AdminPage {

}
