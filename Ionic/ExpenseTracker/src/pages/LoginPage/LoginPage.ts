import { HomePage } from './../home/home';
import { AuthService } from './../../Services/AuthService';
import { NavController } from 'ionic-angular';
import { Component } from '@angular/core';

@Component({
    selector: 'expenses-login',
    templateUrl: 'LoginPage.html'
})
export class Login {

    userName: string;
    password: string;

    constructor(private navbar: NavController, private authService: AuthService) {

    }

    ValidateUser() {
        if (this.userName == null || this.password == null) {
            alert("UserName or Password Field is empty");
        }
        else {
            if (this.authService.AuthticateUser(this.userName.toLowerCase(), this.password)) {
                this.navbar.push(HomePage);
                this.navbar.setRoot(HomePage);
            }
            else {
                alert("Username or Password is incorrect.");
                this.userName = "";
                this.password = "";
            }
        }
    }
}