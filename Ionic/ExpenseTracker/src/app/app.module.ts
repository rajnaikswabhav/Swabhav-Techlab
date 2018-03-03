import { ExpensesDetails } from './../pages/ExpensesDetails/ExpensesDetails';
import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';
import { SplashScreen } from '@ionic-native/splash-screen';
import { StatusBar } from '@ionic-native/status-bar';

import { MyApp } from './app.component';
import { HomePage } from '../pages/home/home';
import { ExpenseSevice } from '../Services/ExpenseService';

@NgModule({
  declarations: [
    MyApp,
    HomePage,
    ExpensesDetails
  ],
  imports: [
    BrowserModule,
    IonicModule.forRoot(MyApp)
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    HomePage,
    ExpensesDetails
  ],
  providers: [
    StatusBar,
    SplashScreen,
    ExpenseSevice,
    {provide: ErrorHandler, useClass: IonicErrorHandler}
  ]
})
export class AppModule {}
