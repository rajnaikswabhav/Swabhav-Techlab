import { Injectable } from "@angular/core";


@Injectable()
export class ExpenseSevice {

    listOfExpense = [{
        id: 101,
        money: 1000,
        catagory: 'Food',
        description: 'Lunch',
        date: '2018-02-17',
    },
    {
        id: 102,
        money: 15000,
        catagory: 'Travel',
        description: 'Travel for work',
        date: '2017-12-10',
    },
    {
        id: 103,
        money: 30000,
        catagory: 'Maintance',
        description: 'Maintance of Machiens',
        date: '2017-10-28',
    },
    {
        id: 104,
        money: 10000,
        catagory: 'Salary',
        description: 'Salary of employees',
        date: '2018-02-07',
    },
    ]

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
                console.log("Inside if...");
                this.listOfExpense[e].id = exp.id;
                this.listOfExpense[e].money = exp.money;
                this.listOfExpense[e].catagory = exp.catagory;
                this.listOfExpense[e].description = exp.description;
                this.listOfExpense[e].date = exp.date;
                break;
            }
        }
    }

    AddExpensesData(exp){
        this.listOfExpense.push(exp);
    }

    DeleteExpensesData(id) {
        for (let e = 0; e < this.listOfExpense.length; e++) {
            if (this.listOfExpense[e].id == id) {
                this.listOfExpense.splice(e, 1);
            }
        }
    }
}