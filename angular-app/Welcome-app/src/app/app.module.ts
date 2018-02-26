import { QuizGameService } from './QiuzGame/Service/QuizGameService';
import { SummaryPipe } from './CustomPipe/SummaryPipe';
import { ToggeLButtonComponent } from './Master-Child-Component/ToggelButtonComponent';
import { MasterComponent } from './Master-Child-Component/MasterComponent';
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
import { StarComponent } from './Master-Child-Component/StarComponent';
import { CustomPipeComponent } from './CustomPipe/CustomPipeComponent';
import { QuizGameComponent } from './QiuzGame/QuizGameComponent';


@NgModule({
  declarations: [
    welcomeComponent,
    StudentComponent,
    BlueboxComponent,
    TwoWayComponent,
    BindingComponent,
    BlogComponent,
    MasterComponent,
    ToggeLButtonComponent,
    StarComponent,
    SummaryPipe,
    CustomPipeComponent,
    QuizGameComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  providers: [
    MathService,
    NumberApiService,
    QuizGameService
  ],
  bootstrap: [welcomeComponent]
})
export class AppModule { }
