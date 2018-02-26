import { CarrerComponent } from './Components/CarrerComponent';
import { AboutComponent } from './Components/AboutComponent';
import { HomeComponents } from './Components/HomeComponent';
import { routesArray } from './RouteConfig';
import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './app.component';
import { CarrerIdComponent } from './Components/CarrerIdComponent';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponents,
    AboutComponent,
    CarrerComponent,
    CarrerIdComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routesArray)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
