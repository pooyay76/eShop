import { Component } from '@angular/core';
import { CartService } from 'src/app/cart/cart.service';
@Component({
  selector: 'app-bill-summary',
  templateUrl: './bill-summary.component.html',
  styleUrls: ['./bill-summary.component.scss']
})
export class BillSummaryComponent {
  constructor(public cartService: CartService) {

  }
}
