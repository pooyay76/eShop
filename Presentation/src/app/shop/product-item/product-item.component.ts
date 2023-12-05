import { Component, Input } from '@angular/core';
import { CartService } from 'src/app/cart/cart.service';
import { Product } from 'src/app/shared/models/product';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})

export default class ProductItemComponent {
  @Input() product?: Product;
  constructor(private cartService: CartService) { }
  addProductToCart() {
    if (this.product)
      this.cartService.addProductToCart(this.product, 1);
  }
}
