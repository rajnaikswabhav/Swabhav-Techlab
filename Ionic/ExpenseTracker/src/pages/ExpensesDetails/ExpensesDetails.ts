import { ExpenseSevice } from './../../Services/ExpenseService';
import { Component } from '@angular/core';
import { NavController } from 'ionic-angular';
import { NavParams } from 'ionic-angular/navigation/nav-params';
import { AlertController } from 'ionic-angular/components/alert/alert-controller';

@Component({
    selector: 'expenses-details',
    templateUrl: 'ExpensesDetails.html'
})
export class ExpensesDetails {

    FLAG = 0;
    id: number;
    money: number;
    catagory: string;
    description: string;
    date: Date;
    exp: any;
    catagories = [];
    validate: string;

    constructor(private navBar: NavController, private navParams: NavParams,
        private service: ExpenseSevice, private alertCtrl: AlertController) {
    }

    ngOnInit() {
        this.exp = this.navParams.data;
        this.id = this.exp.id;
        this.money = this.exp.money;
        this.catagory = this.exp.catagory;
        this.description = this.exp.description;
        this.date = this.exp.date;
        this.catagories = ["Food", "Travel", "Maintance", "Salary", "Infrastructure", "Electricity", "Labor Work"];
        this.FLAG = this.navParams.get('flag');
        if (this.FLAG == 1) {
            this.id = this.navParams.get('id');
        }
    }

    CloseEdit() {
        this.navBar.pop();
    }

    SaveEditData(id, money, catagory, description, date) {

        if (money == null || catagory == null || description == "" || date == null) {
            alert("Something is missing...");
        }
        else {
            let newExp = {
                id: id,
                money: money,
                catagory: catagory,
                description: description,
                date: date,
            }
            if (this.FLAG == 1) {
                this.service.AddExpensesData(newExp);
                this.navBar.pop();
            }
            else {
                this.service.EditExpensesData(newExp);
                this.navBar.pop();
            }
        }
    }

    DeleteData(id) {
        let alert = this.alertCtrl.create({
            title: 'Delete Item',
            message: 'Are you sure, you want to delete this item?',
            buttons: [
                {
                    text: 'DISAGREE',
                    role: 'cancel',
                    handler: () => {
                        console.log("Cansel clicked");
                    }
                },
                {
                    text: 'AGREE',
                    handler: () => {
                        this.service.DeleteExpensesData(id);
                        this.navBar.pop();
                    }
                }
            ]
        });
        alert.present();
    }

}