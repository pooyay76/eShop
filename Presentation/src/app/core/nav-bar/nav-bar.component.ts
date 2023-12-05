import { Component, OnInit } from '@angular/core';
import { CartService } from 'src/app/cart/cart.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent {
  totalCartItems?= 0;
  constructor(private cartService: CartService) {

  }
  ngOnInit(): void {
    this.cartService.cartSource$.subscribe({
      next: resp => this.totalCartItems = resp?.items.reduce((sum, element) => sum + element.quantity, 0)
    });
  }

}
