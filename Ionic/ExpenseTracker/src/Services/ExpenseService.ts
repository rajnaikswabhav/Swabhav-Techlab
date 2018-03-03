import { Injectable } from "@angular/core";
import { Storage } from "@ionic/storage";


@Injectable()
export class ExpenseSevice {

    listOfExpense = [];

    constructor(private storage: Storage) {
        if (this.listOfExpense.length != null) {
            this.storage.get('expenses')
                .then(m => {
                    let expenses = JSON.parse(m);
                    for (let expense of expenses) {
                        this.listOfExpense.push(expense);
                    }
                })
                .catch(e => console.log("Error is", e));
        }
    }

    GetById(id) {

        for (let e of this.listOfExpense) {
            if (e.id == id) {
                let expense = e;
                return expense;
            }
        }
    }

    EditExpensesData(exp) {
        for (let e = 0; e < this.listOfExpense.length; e++) {
            if (this.listOfExpense[e].id == exp.id) {
                this.listOfExpense[e].id = exp.id;
                this.listOfExpense[e].money = exp.money;
                this.listOfExpense[e].catagory = exp.catagory;
                this.listOfExpense[e].description = exp.description;
                this.listOfExpense[e].date = exp.date;
                break;
            }
        }

        this.storage.set('expenses', JSON.stringify(this.listOfExpense));
    }

    AddExpensesData(exp) {
        this.listOfExpense.push(exp);
        this.storage.set('expenses', JSON.stringify(this.listOfExpense));
    }

    DeleteExpensesData(id) {
        for (let e = 0; e < this.listOfExpense.length; e++) {
            if (this.listOfExpense[e].id == id) {
                this.listOfExpense.splice(e, 1);
            }
        }
        this.storage.set('expenses', JSON.stringify(this.listOfExpense));
    }
}