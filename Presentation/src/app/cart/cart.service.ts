import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Cart, CartItem } from '../shared/models/cart';
import { Product } from '../shared/models/product';
import { Bill } from '../shared/models/bill';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private baseUrl = environment.apiUrl;
  private cartSource = new BehaviorSubject<Cart | null>(null);
  private billSource = new BehaviorSubject<Bill | null>(null);
  billSource$ = this.billSource.asObservable();
  cartSource$ = this.cartSource.asObservable();
  bill: Bill = { shippingFee: 0, subTotal: 0, total: 0 };
  constructor(private http: HttpClient) {
    this.cartSource$.subscribe({ next: resp => resp ? this.billSource.next(this.calculateBill(resp)) : undefined });

  }
  private calculateBill(cart: Cart): Bill {
    const shippingFee = 200;
    const subTotal = cart.items.reduce((sum, element) => sum + (element.price * element.quantity), 0);
    this.bill = { shippingFee: shippingFee, subTotal: subTotal, total: subTotal + shippingFee };
    return this.bill;
  }

  removeFromCart(itemId: number) {
    const cart = this.getCurrentCartValue();
    if (cart) {
      cart.items = cart?.items.filter(x => x.id != itemId);
      this.setCart(cart);
    }
  }
  decrementCartItem(itemId: number, quantity = 1) {
    const cart = this.getCurrentCartValue() ?? this.createCart();
    const cartItem = cart.items.find(x => x.id == itemId);

    if (cartItem === undefined)
      return;
    else
      (cartItem.quantity - quantity) < 1 ? this.removeFromCart(itemId) : cartItem.quantity -= quantity;

    this.setCart(cart);
  }

  getCart(id: string) {
    return this.http.get<Cart>(this.baseUrl + "cart?id=" + id).subscribe({
      next: response => this.cartSource.next(response),
      error: err => console.log(err)
    })

  }
  setCart(cart: Cart) {

    return this.http.post<Cart>(this.baseUrl + "cart", cart).subscribe({
      next: response => this.cartSource.next(response),
      error: err => console.log(err)
    })
  }
  createCart(): Cart {
    const cart = new Cart();
    localStorage.setItem("cart-id", cart.id);
    return cart;
  }
  getCurrentCartValue() {
    return this.cartSource.value;
  }
  addProductToCart(product: Product, quantity: number = 1) {

    const newItem = this.mapProductToCartItem(product, quantity);

    const cart = this.getCurrentCartValue() ?? this.createCart();
    cart.items = this.addOrUpdateCartItem(cart.items, newItem);
    this.setCart(cart);
  }

  incrementCartItem(itemId: number, quantity = 1) {
    const cart = this.getCurrentCartValue() ?? this.createCart();
    const cartItem = cart.items.find(x => x.id == itemId);

    if (cartItem === undefined) {
      return;
    }
    else {

      cartItem.quantity += quantity;
    }
    this.setCart(cart);
  }
  private addOrUpdateCartItem(cartItems: CartItem[], newItem: CartItem): CartItem[] {
    const element = cartItems.find(x => x.id == newItem.id);
    if (element) element.quantity += newItem.quantity;
    else cartItems.push(newItem);
    return cartItems;
  }
  private mapProductToCartItem(product: Product, quantity = 0): CartItem {
    return {
      id: product.id,
      pictureUrl: product.pictureUrl,
      productName: product.name,
      price: product.price,
      type: product.typeName,
      quantity: quantity,
      brand: product.brandName
    };
  }
}


