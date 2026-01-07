import { Component, input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { InputNumberModule } from 'primeng/inputnumber';


@Component({
  selector: 'app-cart-item-qty',
  imports: [InputNumberModule, FormsModule],
  templateUrl: './cart-item-qty.html',
  styleUrl: './cart-item-qty.scss',
})
export class CartItemQty {

  count: number = 1;

  ngModelChange(count: number) {
    this.count = count;
    // this.purService.getAll(PriceSort.None).subscribe(gifts => {
    //     this.gifts = gifts;
    //   });
  }
      

}
