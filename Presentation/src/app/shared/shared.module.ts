import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './paging-header/paging-header.component';
import { PagerComponent } from './pager/pager.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { BillSummaryComponent } from './bill-summary/bill-summary.component';



@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent,
    BillSummaryComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot()
  ],
  exports: [
    PagingHeaderComponent,
    PagerComponent,
    CarouselModule,
    BillSummaryComponent
  ]
})
export class SharedModule { }
