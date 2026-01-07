import { Component, EventEmitter, Input, input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { InputNumberModule } from 'primeng/inputnumber';


@Component({
  selector: 'app-cart-item-qty',
  imports: [InputNumberModule, FormsModule],
  templateUrl: './cart-item-qty.html',
  styleUrl: './cart-item-qty.scss',
})
export class CartItemQty {

  @Input() count: number = 1;
  @Output() countChange = new EventEmitter<number>();

  onCountChange(count: number) {
    this.count = count;
    this.countChange.emit(this.count);
  }
      

}
