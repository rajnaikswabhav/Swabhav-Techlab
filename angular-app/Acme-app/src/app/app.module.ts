import { ProductDataService } from './Service/ProductDataService';
import { routesArray } from './RouteConfig';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import {RouterModule} from '@angular/router';
import { AppComponent } from './app.component';
import { WelcomeComponent } from './Components/WelcomeComponent';


@NgModule({
  declarations: [
    AppComponent,
    WelcomeComponent,
    ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routesArray),
  ],
  providers: [ProductDataService],
  bootstrap: [AppComponent]
})
export class AppModule { }
