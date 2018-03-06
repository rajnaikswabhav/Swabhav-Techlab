import { UUID } from 'angular2-uuid';
import { ExpensesDetails } from './../ExpensesDetails/ExpensesDetails';
import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { ExpenseSevice } from '../../Services/ExpenseService';
import { NavParams } from 'ionic-angular/navigation/nav-params';
import { Login } from '../LoginPage/LoginPage';

@Component({
  selector: 'expenses-home',
  templateUrl: 'home.html'
})
export class HomePage {

  expenseList = [];
  FLAG = 0;
  userName: string;
  constructor(public navCtrl: NavController, private expenseService: ExpenseSevice,
    private navParams: NavParams) {
  }

  ngOnInit() {
    this.expenseList = this.expenseService.listOfExpense;
    this.userName = this.navParams.get('name');
    console.log(this.userName);
  }

  OnSelect(id) {
    console.log(id);
    let exp = this.expenseService.GetById(id);
    console.log(exp);
    this.navCtrl.push(ExpensesDetails, exp);
  }

  AddExpenses() {
    this.FLAG = 1;
    this.navCtrl.push(ExpensesDetails, {
      flag: this.FLAG,
      id: UUID.UUID()
    });
  }

  Logout() {
    this.navCtrl.popToRoot();
    this.navCtrl.setRoot(Login);
  }
}
