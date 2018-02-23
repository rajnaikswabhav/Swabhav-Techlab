import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { welcomeComponent } from './Welcome/welcomeComponent';
import { StudentComponent } from './Student/StudentComponent';
import { BlueboxComponent } from './BlueBox/BlueboxComponent';


@NgModule({
  declarations: [
    welcomeComponent,
    StudentComponent,
    BlueboxComponent
  ],
  imports: [
    BrowserModule
  ],
  providers: [],
  bootstrap: [welcomeComponent]
})
export class AppModule { }
