import { DashboardComponent } from './Components/DashboardComponent';
import { LoginComponent } from './Components/LoginComponent';
import { HomeComponent } from './Components/HomeComponent';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule} from '@angular/router';

import { AppComponent } from './app.component';
import { ROUTS_ARRAY } from './RouteConfig';
import { UserService } from './Services/UserService';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule,
    RouterModule.forRoot(ROUTS_ARRAY)
  ],
  providers: [UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
