import { UserService } from './../Services/UserService';
import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { UUID } from 'angular2-uuid';

@Component({
    selector: 'shoppingcart-login',
    templateUrl: 'LoginComponent.html'
})
export class LoginComponent {
    title = 'Shopping Cart';
    FLAG = 0;
    login = "active";
    register: string;
    ROLE = "ADMIN";
    male = "MALE";
    female = "Female"
    data = [];
    userName = "akashmalaviya101@gmail.com";
    password = "akash" ;

    constructor(private userService: UserService, private router: Router) { }

    Login() {
        this.FLAG = 0;
        this.login = "active";
        this.register = "";
    }

    Register() {
        this.FLAG = 1;
        this.register = "active";
        this.login = "";
    }

    LoginAdmin(userName, password) {
        console.log("Inside Login");
        this.userService.AuthniticateUser(userName,password,this.ROLE); 
    }

    RegisterAdmin(firstName, lastName, mobileNo, age, email, password, confirmPassword, gender) {
        console.log(firstName + " " + lastName + " " + gender);
        let newUser = {
            Id: UUID.UUID(),
            FirstName: firstName,
            LastName: lastName,
            PhoneNo: mobileNo,
            Age: age,
            Gender: gender,
            Role: this.ROLE,
            Email: email,
            Password: password,
            ProfilePhoto: "https://www.pexels.com/photo/red-garden-plant-green-56866"
        }

        this.userService.RegisterAdmin(newUser)
            .then(r => { alert("Response status : " + r.status) })
            .catch(r => { console.log(r) });
    }
}