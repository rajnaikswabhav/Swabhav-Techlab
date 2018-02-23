import { TwoWayComponent } from './TwoWay/TwoWayComponent';
import { BrowserModule } from '@angular/platform-browser';
import {FormsModule} from '@angular/forms';
import { NgModule } from '@angular/core';


import { welcomeComponent } from './Welcome/welcomeComponent';
import { StudentComponent } from './Student/StudentComponent';
import { BlueboxComponent } from './BlueBox/BlueboxComponent';


@NgModule({
  declarations: [
    welcomeComponent,
    StudentComponent,
    BlueboxComponent,
    TwoWayComponent
  ],
  imports: [
    BrowserModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [welcomeComponent]
})
export class AppModule { }
