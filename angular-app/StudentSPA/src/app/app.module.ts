import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms'
import { HttpModule } from '@angular/http';

import { AddStudentComponent } from './Components/AddStudentComponent';
import { HomeComponent } from './Components/HomeComponent';
import { DeleteComponent } from './Components/DeleteComponent';
import { EditComponent } from './Components/EditComponent';

import { routesArray } from './RouteConfig';
import { StudentService } from './Service/StudentService';

import { AppComponent } from './app.component';




@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AddStudentComponent,
    DeleteComponent,
    EditComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    FormsModule,
    RouterModule.forRoot(routesArray),
  ],
  providers: [StudentService],
  bootstrap: [AppComponent]
})
export class AppModule { }
