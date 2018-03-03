import { UUID } from 'angular2-uuid';
import { ExpensesDetails } from './../ExpensesDetails/ExpensesDetails';
import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { ExpenseSevice } from '../../Services/ExpenseService';

@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})
export class HomePage {

  expenseList = [];
  FLAG = 0;
  constructor(public navCtrl: NavController, private expenseService: ExpenseSevice) {
  }

  ngOnInit() {
    this.expenseList = this.expenseService.listOfExpense;
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
      id : UUID.UUID() });
  }
}
