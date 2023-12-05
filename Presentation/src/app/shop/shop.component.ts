import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild("search") searchElement?: ElementRef;
  products: Product[] = [];
  brands: Brand[] = [];
  types: Type[] = [];
  count: number = 0;
  shopParams = new ShopParams();
  sortOptions = [
    { name: "Date: New to old", value: "dateDesc" },
    { name: "Date: Old to new", value: "dateAsc" },
    { name: "Name: Ascending", value: "nameAsc" },
    { name: "Name: Descending", value: "nameDesc" },
    { name: "Price: High to low", value: "priceDesc" },
    { name: "Price: Low to high", value: "priceAsc" }
  ];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getProducts();
    this.getTypes();
    this.getBrands();
  }
  private getBrands() {
    this.shopService.getBrands().subscribe({
      next: response => this.brands = [{ id: 0, name: "All" }, ...response],
      error: response => console.log(response)
    });
  }

  private getTypes() {
    this.shopService.getTypes().subscribe({
      next: response => this.types = [{ id: 0, name: "All" }, ...response],
      error: response => console.log(response)
    });
  }

  private getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: response => {
        this.products = response.data,
          this.shopParams.pageIndex = response.pageIndex,
          this.shopParams.pageSize = response.pageSize,
          this.count = response.count
      },
      error: response => console.log(response)
    });
  }
  onBrandFilterSelection(brandId: number) {
    this.shopParams.brandId = brandId;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }
  onTypeFilterSelection(typeId: number) {
    this.shopParams.typeId = typeId;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }
  onSortSelection(event: any) {
    this.shopParams.pageIndex = 1;
    this.shopParams.sortOption = event.target.value;
    this.getProducts();
  }
  onSearch() {
    this.shopParams.search = this.searchElement?.nativeElement.value;
    this.shopParams.pageIndex = 1;
    this.getProducts();
  }
  onReset() {
    this.shopParams = new ShopParams();
    this.getProducts();
  }
  onPageClick(pageNumber: number) {
    if (this.shopParams.pageIndex != pageNumber) {
      this.shopParams.pageIndex = pageNumber;
      this.getProducts();
    }
  }
}
