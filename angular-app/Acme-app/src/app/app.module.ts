import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {RouterModule} from '@angular/router';
import { HttpModule } from '@angular/http';

import { ProductDataService } from './Service/ProductDataService';
import { ROUTES_ARRAY } from './RouteConfig';
import { AppComponent } from './app.component';
import { WelcomeComponent } from './Components/WelcomeComponent';
import { ProductListComponent } from './Components/ProductListComponent';
import { StarComponent } from './Components/StarComponent';
import { ProductDetailComponent } from './Components/ProductDetailComponent';



@NgModule({
  declarations: [
    AppComponent,
    WelcomeComponent,
    ProductListComponent,
    StarComponent,
    ProductDetailComponent
    ],
  imports: [
    BrowserModule,
    HttpModule,
    RouterModule.forRoot(ROUTES_ARRAY),
  ],
  providers: [ProductDataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
