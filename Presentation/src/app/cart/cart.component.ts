import { Component, OnInit } from '@angular/core';
import { CartService } from './cart.service';
import { CartItem } from '../shared/models/cart';
import { Product } from '../shared/models/product';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {
  constructor(public cartService: CartService) { }
  ngOnInit(): void {
  }
  removeCartItem(id: number) {
    this.cartService.removeFromCart(id);
  }
  incrementCartItem(itemId: number) {
    this.cartService.incrementCartItem(itemId, 1);
  }

  decrementCartItem(itemId: number) {
    this.cartService.decrementCartItem(itemId, 1);

  }
}
