import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'Shopping Cart';
  FLAG = 0;
  login = "active";
  register:string;
  Role = "ADMIN";
  male = "MALE";
  female = "Female"


  Login(){
    this.FLAG = 0;
    this.login = "active";
    this.register = "";
  }

  Register(){
    this.FLAG = 1;
    this.register = "active";
    this.login = "";
  }

  LoginAdmin(){
    
  }

  RegisterAdmin(firstName,lastName,mobileNo,age,email,password,confirmPassword,gender){
    console.log(firstName+" "+lastName+" "+gender);
  }
}
