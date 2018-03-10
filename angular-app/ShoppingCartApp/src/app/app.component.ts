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
    console.log("Inside Login");
  }

  RegisterAdmin(){
    console.log("Inside Register");
  }
}
