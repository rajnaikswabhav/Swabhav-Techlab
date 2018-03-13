import { Component } from '@angular/core';
import {UUID} from 'angular2-uuid';
import { Router } from '@angular/router';
import { UserService } from './Services/UserService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent {

  user : {};
  constructor(private router : Router,private userService : UserService){

  }

  ngOnInit(){
     this.user = JSON.parse(localStorage.getItem('user'));

    if(this.user == null){
      this.router.navigate(['login']);
    }
  }

  Logout() {
    alert("Logout");
    setTimeout(() => {
        this.userService.Logout();
        this.router.navigateByUrl('login');
    }, 2000);
}
}
