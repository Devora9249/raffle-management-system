import { Component } from '@angular/core';
import { AddProductButton } from '../add-product-button/add-product-button';
import { SortPanel } from '../sort-panel/sort-panel';

@Component({
  selector: 'app-gifts-header',
  imports: [AddProductButton, SortPanel],
  templateUrl: './gifts-header.html',
  styleUrl: './gifts-header.scss',
})
export class GiftsHeader {

}
