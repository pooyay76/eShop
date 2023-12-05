import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { NotFoundComponent } from './core/not-found/not-found.component';

const routes: Routes = [
  { path: "", component: HomeComponent, pathMatch: 'full', data: { breadcrumb: "Home" } },
  { path: "test-error", component: TestErrorComponent },
  { path: "server-error", component: ServerErrorComponent },
  { path: "not-found", component: NotFoundComponent },
  { path: "shop", loadChildren: () => import("./shop/shop-routing.module").then(m => m.ShopRoutingModule) },
  { path: "cart", loadChildren: () => import("./cart/cart-routing.module").then(m => m.CartRoutingModule) },
  { path: "**", redirectTo: "", pathMatch: "full" }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }