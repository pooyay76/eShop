import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartComponent } from './cart.component';
import { CartRoutingModule } from './cart-routing.module';
import { SharedModule } from '../shared/shared.module';
import { BillSummaryComponent } from '../shared/bill-summary/bill-summary.component';



@NgModule({
  declarations: [
    CartComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    CartRoutingModule,
    SharedModule
  ],
  exports: [
    CartComponent
  ]
})
export class CartModule { }
