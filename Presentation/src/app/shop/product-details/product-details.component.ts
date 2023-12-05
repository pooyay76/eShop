import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { Product } from 'src/app/shared/models/product';
import { BreadcrumbService } from 'xng-breadcrumb';
import { take } from 'rxjs';
import { CartService } from 'src/app/cart/cart.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  quantity = 1;
  quantityInCart = 0;
  product?: Product;
  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute,
    private breadcrumbService: BreadcrumbService, private cartService: CartService
  ) {
    this.breadcrumbService.set("@productDetails", " ");

  }
  ngOnInit(): void {
    this.loadProduct();
  }
  loadProduct() {
    const id = this.activatedRoute.snapshot.paramMap.get("id");
    if (id)
      this.shopService.getProduct(+id).subscribe({
        next: product => {
          this.product = product;
          //the line below is necesarry for the current breadcrumb service
          //if you change it the breadcrumb title will become the id of the product
          this.breadcrumbService.set("@productDetails", product.name);
          this.cartService.cartSource$.pipe(take(1)).subscribe({
            next: resp => {
              const item = resp?.items.find(x => x.id == product.id);
              if (item) {
                this.quantity = item.quantity;
                this.quantityInCart = item.quantity;
              }
            }
          });
        },
        error: error => console.log(error)
      })
  }
  incrementQuantity() {
    this.quantity++;
  }
  decrementQuantity() {
    if (this.quantity > 0)
      this.quantity--;
  }


  //may create a setProductQuantity() method in cart service and replace the code below
  updateCart() {
    if (this.product) {
      if (this.quantity == this.quantityInCart)
        return;

      else if (this.quantity < this.quantityInCart)
        this.cartService.decrementCartItem(this.product.id, this.quantityInCart - this.quantity);

      else
        if (this.quantityInCart == 0)
          this.cartService.addProductToCart(this.product, this.quantity)
        else
          this.cartService.incrementCartItem(this.product.id, this.quantity - this.quantityInCart);

      this.quantityInCart = this.quantity;
    }
  }
  get buttonText() {
    if (this.quantityInCart != this.quantity)
      return "Update Cart";
    else
      return ""
  }
}



