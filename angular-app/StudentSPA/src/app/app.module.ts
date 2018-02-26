import { AddStudentComponent } from './Components/AddStudentComponent';
import { HomeComponent } from './Components/HomeComponent';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import {FormsModule} from '@angular/forms'
import { AppComponent } from './app.component';
import { routesArray } from './RouteConfig';
import { StudentService } from './Service/StudentService';
import { HttpModule } from '@angular/http';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AddStudentComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule,
    RouterModule.forRoot(routesArray)
  ],
  providers: [StudentService],
  bootstrap: [AppComponent]
})
export class AppModule { }
