import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-paging-header',
  templateUrl: './paging-header.component.html',
  styleUrls: ['./paging-header.component.scss']
})
export class PagingHeaderComponent {
  @Input() count?: number;
  @Input() pageSize?: number;
  @Input() pageIndex?: number;
}
