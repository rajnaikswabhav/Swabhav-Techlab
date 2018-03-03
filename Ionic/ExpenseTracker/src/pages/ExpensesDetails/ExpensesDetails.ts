import { HomePage } from './../home/home';
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
        console.log(this.exp);
    }

    CloseEdit() {
        this.navBar.pop();
    }

    SaveEditData(id, money, catagory, description, date) {
        
        console.log("inside save");
        console.log(id, money, catagory, description, date);
        let newExp = {
            id: id,
            money: money,
            catagory: catagory,
            description: description,
            date: date,
        }
        if(this.FLAG == 1){
            console.log("Inside flag if...");
            this.service.AddExpensesData(newExp);
        }
        else{
            this.service.EditExpensesData(newExp);
            this.navBar.push(HomePage);
        }
    }

    AddData(id, money, catagory, description, date) {
        console.log("inside add");
        console.log(id, money, catagory, description, date);
        let newExp = {
            id: id,
            money: money,
            catagory: catagory,
            description: description,
            date: date,
        }

        this.service.AddExpensesData(newExp);
        this.navBar.push(HomePage);
    }

    DeleteData(id) {
        console.log("Inside delete... ");
        let alert = this.alertCtrl.create({
            title: 'Delete Item',
            message: 'Are you sure, you want to delete this item?',
            buttons: [
                {
                    text: 'DISAGREE',
                    role: 'cansel',
                    handler: () => {
                        console.log("Cansel clicked");
                    }
                },
                {
                    text: 'AGREE',
                    handler: () => {
                        this.service.DeleteExpensesData(id);
                        this.navBar.push(HomePage);
                    }
                }
            ]
        });
        alert.present();
    }
}