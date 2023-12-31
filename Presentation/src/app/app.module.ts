import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { ShopModule } from './shop/shop.module';
import { CartModule } from './cart/cart.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { HomeModule } from './home/home.module';
import { LoadingInterceptor } from './core/interceptors/loading.interceptor';
import { CheckoutModule } from './checkout/checkout.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    ShopModule,
    CartModule,
    HomeModule,
    CheckoutModule
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
    , { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
