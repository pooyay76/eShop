import { Component } from '@angular/core';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-section-header',
  templateUrl: './section-header.component.html',
  styleUrls: ['./section-header.component.scss']
})
export class SectionHeaderComponent {
  public breadcrumbs: any[] = []
  constructor(private breadcrumbService: BreadcrumbService) {
    breadcrumbService.breadcrumbs$.subscribe({ next: response => this.breadcrumbs = response });
  }
}
