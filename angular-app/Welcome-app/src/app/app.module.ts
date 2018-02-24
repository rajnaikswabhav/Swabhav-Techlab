import { BlogComponent } from './Blog/BlogComponent';
import { TwoWayComponent } from './TwoWay/TwoWayComponent';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { NgModule } from '@angular/core';


import { welcomeComponent } from './Welcome/welcomeComponent';
import { StudentComponent } from './Student/StudentComponent';
import { BlueboxComponent } from './BlueBox/BlueboxComponent';
import { BindingComponent } from './Binding/BindingComponent';
import { MathService } from './TwoWay/Service/MathService';
import { NumberApiService } from './TwoWay/Service/NumberApiService';


@NgModule({
  declarations: [
    welcomeComponent,
    StudentComponent,
    BlueboxComponent,
    TwoWayComponent,
    BindingComponent,
    BlogComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  providers: [
    MathService,
    NumberApiService
  ],
  bootstrap: [welcomeComponent]
})
export class AppModule { }
