import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent {

  @Input() pageSize?: number;
  @Input() count?: number;
  currentPage = 1;
  @Output() pageClicked = new EventEmitter<number>();
  onPageClick(event: any) {
    this.pageClicked.emit(event.page);
  }
  setPage(pageNumber: number) {
    this.currentPage = pageNumber;
  }

}
