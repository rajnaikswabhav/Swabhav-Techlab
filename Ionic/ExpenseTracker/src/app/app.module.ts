import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';

import { IonicStorageModule } from '@ionic/storage';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';
import { SplashScreen } from '@ionic-native/splash-screen';
import { StatusBar } from '@ionic-native/status-bar';

import { MyApp } from './app.component';

import { HomePage } from '../pages/home/home';
import { ExpenseSevice } from '../Services/ExpenseService';
import { ExpensesDetails } from './../pages/ExpensesDetails/ExpensesDetails';
import { Login } from '../pages/LoginPage/LoginPage';
import { AuthService } from '../Services/AuthService';

@NgModule({
  declarations: [
    MyApp,
    HomePage,
    ExpensesDetails,
    Login
  ],
  imports: [
    BrowserModule,
    IonicStorageModule.forRoot(),
    IonicModule.forRoot(MyApp),
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    HomePage,
    ExpensesDetails,
    Login
  ],
  providers: [
    StatusBar,
    SplashScreen,
    ExpenseSevice,
    AuthService,
    { provide: ErrorHandler, useClass: IonicErrorHandler }
  ]
})
export class AppModule { }
